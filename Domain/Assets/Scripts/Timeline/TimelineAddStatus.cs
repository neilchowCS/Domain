using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAddStatus : TimelineEvent
{
    public int sourceId;
    public int statusId;

    public TimelineAddStatus(int sourceId, int statusId)
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

        //FIXME no need to pass reference to prefab, sprite
        source.healthBar.AddStatus(source.unitData.baseData.commonRef.statusIconPrefab,
            source.unitData.baseData.commonRef.statusIconList[statusId], statusId);
        
    }
}
