using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDeath : TimelineEvent
{
    /// <summary>
    /// eventId 4
    /// 0 = selfId
    /// </summary>

    public int selfId;

    /// <summary>
    /// eventId 4
    /// 0 = selfId
    /// </summary>
    public TimelineDeath(int selfId)
    {
        eventId = 4;
        this.selfId = selfId;
    }

    public override float[] GetData()
    {
        //Debug.Log("Target");
        return new float[] { selfId };
    }
}
