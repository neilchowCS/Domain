using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public List<UnitData> unitList;
    public UDListScriptableObject dataList;

    public TeamData()
    {
        unitList = new List<UnitData>();
    }
    
    public TeamData(UDListScriptableObject dl, int i)
    {
        dataList = dl;
        unitList = new List<UnitData>();
        if (i == 0)
        {
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            /*
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            */
        }
        else if (i == 1)
        {
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            /*
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            */
        }
    }

    public void AddUnitData(UnitData data)
    {
        unitList.Add(data);
    }

}
