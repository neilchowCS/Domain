using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIndependentData
{
    public UnitDataScriptableObject scriptableObject;
    public int level;

    public UnitIndependentData(int level, UnitDataScriptableObject scriptableObject)
    {
        this.scriptableObject = scriptableObject;
        this.level = level;
    }
}