using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class BUnitHelperFunc
{
    public static BattleUnit GetClosestEnemy(BattleUnit battleUnit)
    {
        List<BattleUnit> eligible;
        if (battleUnit.side == 0)
        {
            eligible = battleUnit.executor.player1;
        }
        else
        {
            eligible = battleUnit.executor.player0;
        }
        eligible = eligible.OrderBy(o => GetBattleUnitDistance(battleUnit, o)).ToList();
        if (eligible.Count > 0)
        {
            return eligible[0];
        }
        return null;
    }

    public static float GetBattleUnitDistance(BattleUnit u1, BattleUnit u2)
    {
        return (Vector3.Distance(u1.position, u2.position));
    }

    public static BattleTile GetSpawnLoc(BattleUnit battleUnit)
    {
        List<BattleTile> total;
        List<BattleTile> eligible = new List<BattleTile>();
        if (battleUnit.side == 0)
        {
            total = battleUnit.executor.battleSpace.tiles0;
        }
        else
        {
            total = battleUnit.executor.battleSpace.tiles1;
        }
        foreach (BattleTile x in total)
        {
            if (!x.occupied)
            {
                return x;
            }
        }
        return (total[0]);
    }

}
