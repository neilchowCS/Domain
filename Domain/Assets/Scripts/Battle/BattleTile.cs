using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTile
{
    public Vector3 position;
    /// <summary>
    /// Changed in BattleUnit TickUpMove.
    /// </summary>
    public bool occupied;

    /// <summary>
    /// Constructor for BattleTile with position x, y, z.
    /// </summary>
    public BattleTile(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z);
        position = pos;
        occupied = false;
    }
}
