using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// eventId 2
/// 0 = selfId
/// 1 = selfTarget
/// </summary>
public class TimelineTarget : TimelineEvent
{
    public int selfId;
    public int selfTarget;

    /// <summary>
    /// eventId 2
    /// 0 = selfId
    /// 1 = selfTarget
    /// </summary>
    public TimelineTarget(int selfId, int selfTarget)
    {
        eventId = 2;
        this.selfId = selfId;
        this.selfTarget = selfTarget;
    }

    public override float[] GetData()
    {
        //Debug.Log("Target");
        return new float[] { selfId, selfTarget };
    }

}
