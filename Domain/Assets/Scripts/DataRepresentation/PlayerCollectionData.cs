using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCollectionData
{
    public List<UnitIndividualData> individualDataList;
    //public List<>

    public PlayerCollectionData()
    {
        individualDataList = new List<UnitIndividualData>();
    }
}
