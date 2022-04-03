using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateState : TimelineEvent
{
    public int x;
    public int y;
    public int z;

    public UpdateState(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override int[] GetData()
    {
        return new int[]{x,y,z};
    }
}