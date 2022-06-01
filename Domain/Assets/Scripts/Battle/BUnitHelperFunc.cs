using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Static helper functions.
/// </summary>
public static class BUnitHelperFunc
{
    public static List<Vector3> GetEnemyPositionList(BattleUnit battleUnit)
    {
        List<Vector3> output = new List<Vector3>();
        foreach (BattleUnit enemy in battleUnit.executor.GetEnemyUnits(battleUnit))
        {
            output.Add(enemy.position);
        }
        return output;
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

        /*
        foreach (BattleTile x in total)
        {
            if (!x.occupied)
            {
                return x;
            }
        }
        return (total[0]);
        */
        
        foreach (BattleTile x in total)
        {
            if (!x.occupied)
            {
                eligible.Add(x);
            }
        }
        int output = Random.Range(0, eligible.Count);
        //Debug.Log(output);
        return (eligible[output]);
        
    }
}
