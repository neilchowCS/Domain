using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon
{
    public IBattleUnit occupant = null;

    private Vector3 position;
    public Vector3 Position
    {
        get { return position + new Vector3(0, 0.5f, 0); }
        set { position = value; }
    }

    public Hexagon(Vector3 position)
    {
        this.position = position;
    }
}
