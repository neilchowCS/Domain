using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitIndividualCollection
{
    public List<UnitIndividualData> collection;

    public UnitIndividualCollection()
    {
        collection = new List<UnitIndividualData>();
    }
}
