using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0 = selfId
/// 1 = selfTarget
/// </summary>
public class TimelineTarget : TimelineEvent
{
    public int selfId;
    public int selfTarget;

    /// <summary>
    /// 0 = selfId
    /// 1 = selfTarget
    /// </summary>
    public TimelineTarget(int selfId, int selfTarget)
    {
        this.selfId = selfId;
        this.selfTarget = selfTarget;
    }

    public override float[] GetData()
    {
        return new float[] { selfId, selfTarget };
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayObject self = null;
        ReplayObject target = null;
        foreach (ReplayObject rO in replayExecutor.replayObjects)
        {
            if (rO.globalId == selfId)
            {
                self = rO;
            }
            else if (rO.globalId == selfTarget)
            {
                target = rO;
            }
        }
        if (self != null && target != null)
        {
            self.gameObject.GetComponent<ReplayUnit>().target = target;
        }
        else
        {
            Debug.Log("Change target failed");
        }
    }

}
