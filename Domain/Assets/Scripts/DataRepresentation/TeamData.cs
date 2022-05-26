using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public List<UnitRuntimeData> unitList;
    public List<int> positionList;

    public TeamData()
    {
        unitList = new List<UnitRuntimeData>();
        positionList = new List<int>();
    }

    public TeamData(PrimitiveTeamData primitive, UDListScriptableObject uDList)
    {
        unitList = new List<UnitRuntimeData>();
        foreach (UnitIndividualData individualData in primitive.dataList)
        {
            unitList.Add(new UnitRuntimeData((uDList.uDList[individualData.unitId], individualData)));
        }
        positionList = primitive.positionList;
    }

    public void AddUnitData(UnitRuntimeData data, int i)
    {
        unitList.Add(data);
        positionList.Add(i);
    }

    public void RefreshRuntimeData()
    {
        List<UnitRuntimeData> newUnitList = new List<UnitRuntimeData>();
        for (int i = 0; i < unitList.Count; i++)
        {
            newUnitList.Add(new UnitRuntimeData((unitList[i].baseData, unitList[i].individualData)));
        }
        unitList = newUnitList;
    }
}
