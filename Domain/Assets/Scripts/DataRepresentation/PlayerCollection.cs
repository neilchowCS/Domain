using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerCollection
{
    public List<UnitIndividualData> collectionList;

    public PlayerCollection()
    {
        collectionList = new List<UnitIndividualData>();
    }
}
