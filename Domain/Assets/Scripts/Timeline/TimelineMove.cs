using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// eventId 3
/// 0 = selfId
/// 1 = nextTileId
/// 2 = ntPosX
/// 3 = ntPosY
/// 4 = ntPosZ
/// </summary>
public class TimelineMove : TimelineEvent
{
    public int selfId;
    public int nextTileId;
    public float ntPosX;
    public float ntPosY;
    public float ntPosZ;

    /// <summary>
    /// eventId 3
    /// 0 = selfId
    /// 1 = nextTileId
    /// 2 = ntPosX
    /// 3 = ntPosY
    /// 4 = ntPosZ
    /// </summary>
    public TimelineMove(int selfId, int nextTileId,
        float ntPosX, float ntPosY, float ntPosZ)
    {
        eventId = 3;
        this.selfId = selfId;
        this.nextTileId = nextTileId;
        this.ntPosX = ntPosX;
        this.ntPosY = ntPosY;
        this.ntPosZ = ntPosZ;
    }

    public override float[] GetData()
    {
        //Debug.Log("Move");
        return new float[] { selfId, nextTileId,
        ntPosX, ntPosY, ntPosZ};
    }
}
