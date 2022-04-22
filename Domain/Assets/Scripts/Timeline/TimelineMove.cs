using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
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
    /// 0 = selfId
    /// 1 = nextTileId
    /// 2 = ntPosX
    /// 3 = ntPosY
    /// 4 = ntPosZ
    /// </summary>
    public TimelineMove(int selfId, int nextTileId,
        float ntPosX, float ntPosY, float ntPosZ)
    {
        this.selfId = selfId;
        this.nextTileId = nextTileId;
        this.ntPosX = ntPosX;
        this.ntPosY = ntPosY;
        this.ntPosZ = ntPosZ;
    }

    public override float[] GetData()
    {
        return new float[] { selfId, nextTileId,
        ntPosX, ntPosY, ntPosZ};
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayObject self = null;
        Vector3 destination = new Vector3(ntPosX, ntPosY + .5f, ntPosZ);
        foreach (ReplayObject rO in replayExecutor.replayObjects)
        {
            if (rO.globalId == selfId)
            {
                self = rO;
            }
        }
        if (self != null)
        {
            self.gameObject.GetComponent<ReplayUnit>().destination = destination;
            self.gameObject.GetComponent<ReplayUnit>().moving = true;
        }
        else
        {
            Debug.Log("Movement failed");
        }
    }
}
