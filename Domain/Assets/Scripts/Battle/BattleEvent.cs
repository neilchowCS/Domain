using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEvent
{
    public virtual int[] GetData()
    {
        Debug.Log("Generic Event");
        return null;
    }
}
