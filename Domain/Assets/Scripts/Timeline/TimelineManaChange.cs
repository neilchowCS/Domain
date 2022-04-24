using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManaChange : TimelineEvent
{
    public int sourceId;
    public int newAmount;

    public TimelineManaChange(int sourceId, int newAmount)
    {
        this.sourceId = sourceId;
        this.newAmount = newAmount;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit source = null;
        foreach (ReplayUnit rO in replayExecutor.replayUnits)
        {
            if (rO.globalId == sourceId)
            {
                source = rO;
            }
        }
        if (source != null)
        {
            source.unitData.mana = newAmount;
        }
        else
        {
            Debug.Log("ManaChangeFailed");
        }
    }
}
