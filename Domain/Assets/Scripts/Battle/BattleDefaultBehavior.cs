using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleBehaviorExtension
{
    public static class BattleDefaultBehavior
    {
        public static IBattleUnit GetClosestEnemy(this BattleUnit battleUnit)
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
        public static BattleTile GetNextBattleTile(this BattleUnit battleUnit)
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
        public static void PrepareMovement(this BattleUnit unit)
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
        public static void MoveTowardsNext(this BattleUnit unit)
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

        public static bool TileArrived(this BattleUnit unit)
        {
            return (Vector3.Distance(unit.Position, unit.TargetTile.position) < 0.000001f);
        }
    }
}