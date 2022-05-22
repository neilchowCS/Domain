using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageDataCollection
{
    public List<PrimitiveTeamData> stageDataList;

    public StageDataCollection()
    {
        stageDataList = new List<PrimitiveTeamData>();
    }
}
