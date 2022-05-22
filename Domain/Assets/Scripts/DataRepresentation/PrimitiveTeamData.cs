using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrimitiveTeamData
{
    public List<UnitIndividualData> dataList;
    public List<int> positionList;

    public PrimitiveTeamData()
    {
        dataList = new List<UnitIndividualData>();
        positionList = new List<int>();
    }
}
