using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStatusData
{
    public float duration;
    public bool isPercent;
    public float value0;
    public float value1;

    public SimpleStatusData(float duration, bool isPercent, float value0)
    {
        this.duration = duration;
        this.isPercent = isPercent;
        this.value0 = value0;
        value1 = 0;
    }

    public SimpleStatusData(float duration, bool isPercent, float value0, float value1)
    {
        this.duration = duration;
        this.isPercent = isPercent;
        this.value0 = value0;
        this.value1 = value1;
    }
}
