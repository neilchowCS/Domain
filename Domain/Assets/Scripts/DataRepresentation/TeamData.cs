using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData
{
    public UDListScriptableObject dataList;

    public List<UnitData> unitList;
    public List<int> positionList;

    public TeamData()
    {
        unitList = new List<UnitData>();
        positionList = new List<int>();
    }
    
    public TeamData(UDListScriptableObject dl, int i)
    {
        dataList = dl;
        unitList = new List<UnitData>();
        positionList = new List<int>();
        if (i == 0)
        {
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            /*
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            */
        }
        else if (i == 1)
        {
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[1]));
            /*
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            AddUnitData(new UnitData(dataList.uDList[0]));
            */
        }
    }

    public void AddUnitData(UnitData data)
    {
        unitList.Add(data);
    }

    public void AddUnitData(UnitData data, int i)
    {
        unitList.Add(data);
        positionList.Add(i);
    }

}
