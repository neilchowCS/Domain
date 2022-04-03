using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTile
{
    public Vector3 position;
    public bool occupied;

    public BattleTile(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z);
        position = pos;
        occupied = false;
    }
}
