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

    public void AddUnitData(UnitRuntimeData data, int i)
    {
        unitList.Add(data);
        positionList.Add(i);
    }
}
