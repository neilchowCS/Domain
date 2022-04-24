using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIndependentData
{
    public UnitDataScriptableObject baseData;
    public int level;

    public UnitIndependentData(UnitDataScriptableObject baseData)
    {
        this.baseData = baseData;
    }

    public UnitIndependentData(int level, UnitDataScriptableObject baseData)
    {
        this.baseData = baseData;
        this.level = level;
    }
}