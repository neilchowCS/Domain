using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ActionExtension
{
    public static class MovementExtension
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
        public static int GetNextBattleTile(this IBattleUnit battleUnit)
        {
            Vector3 position1 = battleUnit.Position;
            Vector3 position2 = battleUnit.CurrentTarget.Position;

            int currTile = battleUnit.Tile;

            List<int> eligible = new();

            foreach (int x in battleUnit.Executor.mapGraph[currTile].connections)
            {
                if (!battleUnit.Executor.mapGraph[x].occupied)
                {
                    eligible.Add(x);
                }
            }
            //get adjacent tile with least distance between it and the target
            eligible = eligible.OrderBy(o => Vector3.Distance(battleUnit.Executor.mapGraph[o].Position, position2)).ToList();
            if (eligible.Count > 0) //&& Vector3.Distance(eligible[0].position, position2) < Vector3.Distance(currTile.position, position2))
            {
                return eligible[0];
            }
            //default
            return currTile;
        }
    }
}