using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineRemoveStatus : TimelineEvent
{
    public int hostId;
    public int statusId;

    public TimelineRemoveStatus(int hostId, int statusId)
    {
        this.hostId = hostId;
        this.statusId = statusId;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit host = null;
        foreach (ReplayUnit rO in replayExecutor.replayUnits)
        {
            if (rO.globalId == hostId)
            {
                host = rO;
                break;
            }
        }

        host.healthBar.RemoveStatus(statusId);
    }
}
