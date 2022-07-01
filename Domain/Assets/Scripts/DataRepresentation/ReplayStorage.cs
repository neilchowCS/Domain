using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayStorage
{
    public List<PrimitiveTeamData> team1List;
    public List<PrimitiveTeamData> team2List;
    public List<UnityEngine.Random.State> seedList;

    public ReplayStorage()
    {
        team1List = new List<PrimitiveTeamData>();
        team2List = new List<PrimitiveTeamData>();
        seedList = new List<Random.State>();
    }

    public ReplayStorage(List<PrimitiveTeamData> team1, List<PrimitiveTeamData> team2,
        List<Random.State> seedList)
    {
        team1List = team1;
        team2List = team2;
        this.seedList = seedList;
    }
}
