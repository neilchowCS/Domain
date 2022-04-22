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
        return new float[] { selfId };
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit self = null;
        foreach (ReplayUnit rU in replayExecutor.replayUnits)
        {
            if (rU.globalId == selfId)
            {
                self = rU;
            }
        }
        if (self != null)
        {
            //FIXME play death anim
            self.gameObject.SetActive(false);
            GameObject.Destroy(self.healthBar.gameObject);
        }
    }
}
