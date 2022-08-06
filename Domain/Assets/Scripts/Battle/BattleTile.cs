using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTile
{
    public int id;
    private Vector3 position;
    public Vector3 Position { get { return position + new Vector3(0,0.5f,0); }
        set { position = value; } }
    /// <summary>
    /// Changed in BattleUnit TickUpMove.
    /// </summary>
    public bool occupied;

    /// <summary>
    /// Constructor for BattleTile with position x, y, z.
    /// </summary>
    public BattleTile(float x, float y, float z, int id)
    {
        Vector3 pos = new Vector3(x, y, z);
        this.id = id;
        Position = pos;
        occupied = false;
    }
}
