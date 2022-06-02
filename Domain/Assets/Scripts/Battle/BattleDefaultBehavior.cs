using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleBehaviorExtension
{
    public static class BattleDefaultBehavior
    {
        public static BattleUnit GetClosestEnemy(this BattleUnit battleUnit)
        {
            List<BattleUnit> eligible = battleUnit.executor.GetEnemyUnits(battleUnit);
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
            Vector3 position1 = battleUnit.position;
            Vector3 position2 = battleUnit.currentTarget.position;
            List<BattleTile> total = battleUnit.executor.battleSpace.tiles;
            BattleTile currTile = battleUnit.currentTile;
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
            if (eligible.Count > 0 && Vector3.Distance(eligible[0].position, position2) <
                Vector3.Distance(currTile.position, position2))
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
            unit.targetTile = unit.GetNextBattleTile();
            if (unit.targetTile != unit.currentTile)
            {
                unit.executor.timeline.AddTimelineEvent(new TimelineMove(unit.globalObjectId,
                0, unit.targetTile.position.x, unit.targetTile.position.y, unit.targetTile.position.z));

                if (unit.targetTile.occupied)
                {
                    Debug.Log("Uh oh!");
                }

                unit.targetTile.occupied = true;
            }
        }

        /// <summary>
        /// Movement loop
        /// </summary>
        public static void MoveTowardsNext(this BattleUnit unit)
        {
            unit.position = Vector3.MoveTowards(unit.position, unit.targetTile.position,
                unit.unitData.unitMoveSpeed.Value / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(unit.position, unit.currentTile.position)
                < Vector3.Distance(unit.position, unit.targetTile.position))
            {
                unit.currentTile.occupied = false;
                unit.currentTile = unit.targetTile;
            }
        }

        public static bool TileArrived(this BattleUnit unit)
        {
            return (Vector3.Distance(unit.position, unit.targetTile.position) < 0.000001f);
        }
    }
}