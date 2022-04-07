using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public List<UnitData> unitList;

    public TeamData()
    {
        unitList = new List<UnitData>();
    }
    
    public TeamData(int i)
    {
        unitList = new List<UnitData>();
        if (i == 0)
        {
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(1));
            AddUnitData(new UnitData(1));
        }
        else if (i == 1)
        {
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
            AddUnitData(new UnitData(0));
        }
    }

    public void AddUnitData(UnitData data)
    {
        unitList.Add(data);
    }

}
