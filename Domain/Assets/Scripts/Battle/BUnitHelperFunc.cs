using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Static helper functions.
/// </summary>
public static class BUnitHelperFunc
{
    public static List<BattleUnit> GetEnemyList(BattleUnit battleUnit)
    {
        if (battleUnit.side == 0)
        {
            return battleUnit.executor.player1;
        }
        return battleUnit.executor.player0;
    }

    public static List<Vector3> GetEnemyPositionList(BattleUnit battleUnit)
    {
        List<Vector3> output = new List<Vector3>();
        foreach (BattleUnit enemy in GetEnemyList(battleUnit))
        {
            output.Add(enemy.position);
        }
        return output;
    }

    /// <summary>
    /// Returns the closest BattleUnit to param.
    /// </summary>
    public static BattleUnit GetClosestEnemy(BattleUnit battleUnit)
    {
        List<BattleUnit> eligible = GetEnemyList(battleUnit);
        eligible = eligible.OrderBy(o => GetBattleUnitDistance(battleUnit, o)).ToList();
        if (eligible.Count > 0)
        {
            return eligible[0];
        }
        return null;
    }

    /// <summary>
    /// Returns the distance between two BattleUnits.
    /// </summary>
    public static float GetBattleUnitDistance(BattleUnit u1, BattleUnit u2)
    {
        return (Vector3.Distance(u1.position, u2.position));
    }

    /// <summary>
    /// Returns random empty BattleTile on the side of param.
    /// </summary>
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

    /// <summary>
    /// Gets the BattleTile that is adjacent to u1.currentTile and closest to u2.
    /// </summary>
    public static BattleTile GetNextBattleTile(BattleUnit u1, BattleUnit u2)
    {
        Vector3 position1 = u1.position;
        Vector3 position2 = u2.position;
        List<BattleTile> total = u1.executor.battleSpace.tiles;
        BattleTile currTile = u1.currentTile;
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
}
