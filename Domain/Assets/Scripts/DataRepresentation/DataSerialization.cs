using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSerialization
{
    public DataSerialization()
    {

    }

    public string SerializeIndividual(UnitIndividualData individualData)
    {
        string json = JsonUtility.ToJson(individualData);
        return json;
    }
}
