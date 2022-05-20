using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public UDListScriptableObject dataList;

    public List<UnitRuntimeData> unitList;
    public List<int> positionList;

    public TeamData()
    {
        unitList = new List<UnitRuntimeData>();
        positionList = new List<int>();
    }
    
    public TeamData(UDListScriptableObject dl, int i)
    {
        dataList = dl;
        unitList = new List<UnitRuntimeData>();
        positionList = new List<int>();
        if (i == 0)
        {
            AddUnitData(new UnitRuntimeData((dataList.uDList[0], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[0], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[1], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[1], new UnitIndividualData())));
        }
        else if (i == 1)
        {
            AddUnitData(new UnitRuntimeData((dataList.uDList[2], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[2], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[1], new UnitIndividualData())));
            AddUnitData(new UnitRuntimeData((dataList.uDList[1], new UnitIndividualData())));
        }
    }

    public void AddUnitData(UnitRuntimeData data)
    {
        unitList.Add(data);
    }

    public void AddUnitData(UnitRuntimeData data, int i)
    {
        unitList.Add(data);
        positionList.Add(i);
    }

}
