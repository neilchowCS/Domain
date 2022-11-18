using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayRecord
{
    public List<UnitIndividualData> team0Data;
    public List<int> team0Position;

    public List<UnitIndividualData> team1Data;
    public List<int> team1Position;

    public int seed;

    public ReplayRecord(BattleRecord record, int seed)
    {
        team0Data = record.team0Data;
        team0Position = record.team0Position;
        team1Data = record.team1Data;
        team1Position = record.team1Position;
        this.seed = seed;
    }
}
