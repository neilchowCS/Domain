using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle
{
    public float radius;
    public Vector3 center;
    public Vector3 chord1;
    public Vector3 chord2;

    public Circle(Vector3 center, float radius, Vector3 chord1, Vector3 chord2)
    {
        this.center = center;
        this.radius = radius;
        this.chord1 = chord1;
        this.chord2 = chord2;
    }
}
