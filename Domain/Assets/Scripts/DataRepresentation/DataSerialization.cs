using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSerialization
{
    public DataSerialization()
    {

    }

    public string SerializeCollection(PlayerCollection collection)
    {
        string json = JsonUtility.ToJson(collection, true);
        return json;
    }

    public string SerializeIndividual(UnitIndividualData individualData)
    {
        string json = JsonUtility.ToJson(individualData, true);
        return json;
    }

    public PlayerCollection DeserializeCollection(string text)
    {
        return JsonUtility.FromJson<PlayerCollection>(text);
    }
}
