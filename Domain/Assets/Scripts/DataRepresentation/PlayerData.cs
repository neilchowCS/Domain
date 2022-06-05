using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public UnitIndividualCollection unitCollection;

    public PlayerData()
    {

    }

    public PlayerData(UnitIndividualCollection collection)
    {
        unitCollection = collection;
    }
}
