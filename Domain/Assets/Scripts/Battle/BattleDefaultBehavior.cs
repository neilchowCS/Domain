using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleBehaviorExtension
{
    public static class BattleDefaultBehavior
    {
        public static float GetBattleUnitDistance(this IBattleUnit unit, IBattleUnit otherUnit)
            => Vector3.Distance(unit.Position, otherUnit.Position);

        public static float GetTargetDistance(this IBattleUnit unit)
            => unit.GetBattleUnitDistance(unit.CurrentTarget);

        public static bool TargetInRange(this IBattleUnit unit)
            => unit.GetTargetDistance() < unit.UnitData.unitRange.Value;

        /// <summary>
        /// Sets currentTarget for this BattleUnit.
        /// </summary>
        public static void TargetClosestEnemy(this IBattleUnit unit)
            => unit.CurrentTarget = unit.GetClosestEnemy();

        public static IBattleUnit GetClosestEnemy(this IBattleUnit battleUnit)
        {
            List<IBattleUnit> eligible = battleUnit.Executor.GetEnemyUnits(battleUnit);
            eligible = eligible.OrderBy(o => battleUnit.GetBattleUnitDistance(o)).ToList();
            if (eligible.Count > 0)
            {
                return eligible[0];
            }
            return null;
        }

        /// <summary>
        /// Gets unoccupied BattleTile that is adjacent to unit and closest to current target.
        /// </summary>
        public static BattleTile GetNextBattleTile(this IBattleUnit battleUnit)
        {
            Vector3 position1 = battleUnit.Position;
            Vector3 position2 = battleUnit.CurrentTarget.Position;
            List<BattleTile> total = battleUnit.Executor.battleSpace.tiles;
            BattleTile currTile = battleUnit.CurrentTile;
            List<BattleTile> eligible = new List<BattleTile>();

            float tileDist = 1.75f;

            foreach (BattleTile x in total)
            {
                if (!x.occupied && Vector3.Distance(x.position, currTile.position) < tileDist)
                {
                    eligible.Add(x);
                }
            }
            //get adjacent tile with least distance between it and the target
            eligible = eligible.OrderBy(o => Vector3.Distance(o.position, position2)).ToList();
            if (eligible.Count > 0) //&& Vector3.Distance(eligible[0].position, position2) < Vector3.Distance(currTile.position, position2))
            {
                return eligible[0];
            }
            //default
            return currTile;
        }

        /// <summary>
        /// sets target tile
        /// adds to timeline
        /// </summary>
        public static void PrepareMovement(this IBattleUnit unit)
        {
            unit.TargetTile = unit.GetNextBattleTile();
            if (unit.TargetTile != unit.CurrentTile)
            {
                if (unit.TargetTile.occupied)
                {
                    Debug.Log("Uh oh!");
                }

                unit.TargetTile.occupied = true;
            }
        }

        /// <summary>
        /// Movement loop
        /// </summary>
        public static void MoveTowardsNext(this IBattleUnit unit)
        {
            unit.Position = Vector3.MoveTowards(unit.Position, unit.TargetTile.position,
                unit.UnitData.unitMoveSpeed.Value / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(unit.Position, unit.CurrentTile.position)
                < Vector3.Distance(unit.Position, unit.TargetTile.position))
            {
                unit.CurrentTile.occupied = false;
                unit.CurrentTile = unit.TargetTile;
            }
        }

        public static bool TileArrived(this IBattleUnit unit)
        {
            return (Vector3.Distance(unit.Position, unit.TargetTile.position) < 0.000001f);
        }
    }
}