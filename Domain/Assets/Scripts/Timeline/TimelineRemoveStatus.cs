using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineRemoveStatus : TimelineEvent
{
    public int sourceId;
    public int statusId;

    public TimelineRemoveStatus(int sourceId, int statusId)
    {
        this.sourceId = sourceId;
        this.statusId = statusId;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit source = null;
        foreach (ReplayUnit rO in replayExecutor.replayUnits)
        {
            if (rO.globalId == sourceId)
            {
                source = rO;
                break;
            }
        }

        source.healthBar.RemoveStatus(statusId);
    }
}
