using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSerialization
{
    public DataSerialization()
    {

    }

    public static PlayerCollectionData DeserializeStaticCollection(string s)
    {
        return JsonUtility.FromJson<PlayerCollectionData>(s);
    }

    public static string SerializeStaticCollection(PlayerCollectionData collection)
    {
        string json = JsonUtility.ToJson(collection, true);
        return json;
    }

    public static PlayerData DeserializeStaticPlayerData(string s)
    {
        return JsonUtility.FromJson<PlayerData>(s);
    }

    public static string SerializeStaticPlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        return json;
    }

    public string SerializeData(StageDataCollection stageData)
    {
        string json = JsonUtility.ToJson(stageData, true);
        return json;
    }

    public string SerializeData(PlayerCollectionData collection)
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

    public string SerializeData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        return json;
    }

    public PlayerData DeserializePlayerData(string text)
    {
        return JsonUtility.FromJson<PlayerData>(text);
    }

    public PlayerCollectionData DeserializeCollection(string text)
    {
        return JsonUtility.FromJson<PlayerCollectionData>(text);
    }

    public StageDataCollection DeserializeStageData(string text)
    {
        return JsonUtility.FromJson<StageDataCollection>(text);
    }
}
