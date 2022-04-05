using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEvent
{
    public virtual float[] GetData()
    {
        Debug.Log("Generic Event");
        return new float[]{0};
    }
    
    public virtual void ExecuteEvent(ReplayExecutor replayExecutor)
    {

    }
}
