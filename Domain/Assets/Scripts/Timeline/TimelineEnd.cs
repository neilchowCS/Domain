using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEnd : TimelineEvent
{
    public TimelineEnd()
    {

    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        replayExecutor.replayRun = false;
        replayExecutor.enabled = false;
    }
}
