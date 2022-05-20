using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitIndividualData
{
    public int unitId;
    public int level;

    public UnitIndividualData()
    {

    }

    public UnitIndividualData(int unitId, int level)
    {
        this.unitId = unitId;
        this.level = level;
    }

}