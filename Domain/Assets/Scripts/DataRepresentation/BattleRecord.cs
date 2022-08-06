using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleRecord
{
    public List<UnitIndividualData> team0Data;
    public List<int> team0Position;

    public List<UnitIndividualData> team1Data;
    public List<int> team1Position;

    public BattleRecord()
    {
        team0Data = new();
        team0Position = new();
        team1Data = new();
        team1Position = new();
    }

    public BattleRecord(PrimitiveTeamData stageData)
    {
        team0Data = new();
        team0Position = new();
        team1Data = stageData.dataList;
        team1Position = stageData.positionList;
    }

    public BattleRecord(ReplayRecord record)
    {
        team0Data = record.team0Data;
        team0Position = record.team0Position;
        team1Data = record.team1Data;
        team1Position = record.team1Position;
    }

    public void AddItem(bool isSide0, UnitIndividualData data, int pos)
    {
        if (isSide0)
        {
            team0Data.Add(data);
            team0Position.Add(pos);
        }
        else
        {
            team1Data.Add(data);
            team1Position.Add(pos);
        }
    }


}
