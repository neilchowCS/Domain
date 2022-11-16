using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ActionExtension
{
    public static class MovementExtension
    {
        /*
        public static float GetBattleUnitDistance(this IBattleUnit unit, IBattleUnit otherUnit)
            => Vector3.Distance(unit.Position, otherUnit.Position);
        */
        public static float GetTargetDistance(this IBattleUnit unit)
            => unit.GetBattleUnitDistance(unit.CurrentTarget);
        
        public static bool TargetInRange(this IBattleUnit unit)
            => unit.GetTargetDistance() < unit.UnitData.unitRange.Value;
        

        public static int GetBattleUnitDistance(this IBattleUnit unit, IBattleUnit otherUnit)
        {
            return unit.Executor.hexagonFunctions.GetDistance(unit.X, unit.Y, otherUnit.X, otherUnit.Y);
        }

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
        public static (int,int) GetNextBattleTile(this IBattleUnit battleUnit)
        {
            Vector3 position1 = battleUnit.Position;
            Vector3 position2 = battleUnit.CurrentTarget.Position;

            List<(int,int)> eligible = new();

            List<(int, int)> neighbors = battleUnit.Executor.hexagonFunctions.GetNeighbors(battleUnit.X, battleUnit.Y);
            foreach ((int,int) x in neighbors)
            {
                if (battleUnit.Executor.hexMap[x.Item1, x.Item2].occupant == null)
                {
                    eligible.Add(x);
                }
            }
            //get adjacent tile with least distance between it and the target
            eligible = eligible.OrderBy(o => Vector3.Distance(battleUnit.Executor.hexMap[o.Item1,o.Item2].Position, position2)).ToList();
            if (eligible.Count > 0) //&& Vector3.Distance(eligible[0].position, position2) < Vector3.Distance(currTile.position, position2))
            {
                return eligible[0];
            }
            //default
            return (battleUnit.X, battleUnit.Y);
        }
    }
}