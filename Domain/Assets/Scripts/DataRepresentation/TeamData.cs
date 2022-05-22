using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public List<UnitIndividualData> unitList;
    public List<int> positionList;

    public TeamData()
    {
        unitList = new List<UnitIndividualData>();
        positionList = new List<int>();
    }
    
    public TeamData(int i)
    {
        positionList = new List<int>();
        if (i == 0)
        {
            AddUnitData(new UnitIndividualData());
            AddUnitData(new UnitIndividualData());
            AddUnitData(new UnitIndividualData());
            AddUnitData(new UnitIndividualData());
        }
    }

    public void AddUnitData(UnitIndividualData data)
    {
        unitList.Add(data);
    }

    public void AddUnitData(UnitIndividualData data, int i)
    {
        unitList.Add(data);
        positionList.Add(i);
    }

    public List<UnitRuntimeData> CreateRuntimeList(UDListScriptableObject UDlistSO)
    {
        List<UnitRuntimeData> output = new List<UnitRuntimeData>();
        foreach (UnitIndividualData unit in unitList)
        {
            output.Add(new UnitRuntimeData((UDlistSO.uDList[unit.unitId], unit)));
        }
        return output;
    }

    public UnitRuntimeData CreateUnitRuntimeData(UDListScriptableObject UDlistSO, int index)
    {
        return new UnitRuntimeData((UDlistSO.uDList[unitList[index].unitId], unitList[index]));
    }
}
