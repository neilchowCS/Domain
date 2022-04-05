using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDeath : TimelineEvent
{
    /// <summary>
    /// 0 = selfId
    /// </summary>

    public int selfId;

    /// <summary>
    /// 0 = selfId
    /// </summary>
    public TimelineDeath(int selfId)
    {
        this.selfId = selfId;
    }

    public override float[] GetData()
    {
        //Debug.Log("Target");
        return new float[] { selfId };
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayObject self = null;
        foreach (ReplayObject rO in replayExecutor.replayObjects)
        {
            if (rO.globalId == selfId)
            {
                self = rO;
            }
        }
        if (self != null)
        {
            replayExecutor.replayObjects.Remove(self);
            replayExecutor.replayUnits.Remove(self.gameObject.GetComponent<ReplayUnit>());
            GameObject.Destroy(self.gameObject);
        }
    }
}
