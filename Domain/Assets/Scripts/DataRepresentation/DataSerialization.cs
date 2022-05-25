using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSerialization
{
    public DataSerialization()
    {

    }

    public string SerializeData(StageDataCollection stageData)
    {
        string json = JsonUtility.ToJson(stageData, true);
        return json;
    }

    public string SerializeData(UnitIndividualCollection collection)
    {
        string json = JsonUtility.ToJson(collection, true);
        return json;
    }

    public string SerializeData(PrimitiveTeamData teamData)
    {
        string json = JsonUtility.ToJson(teamData, true);
        return json;
    }

    public string SerializeData(UnitIndividualData individualData)
    {
        string json = JsonUtility.ToJson(individualData, true);
        return json;
    }

    public UnitIndividualCollection DeserializeCollection(string text)
    {
        return JsonUtility.FromJson<UnitIndividualCollection>(text);
    }

    public StageDataCollection DeserializeStageData(string text)
    {
        return JsonUtility.FromJson<StageDataCollection>(text);
    }
}
