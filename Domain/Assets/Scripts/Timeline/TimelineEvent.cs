using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEvent
{
    public int eventId = 0;

    public virtual float[] GetData()
    {
        Debug.Log("Generic Event");
        return new float[]{0};
    }

    public int GetEventId()
    {
        return eventId;
    }
}
