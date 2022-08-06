using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayStorage
{
    public List<ReplayRecord> replayRecords;

    public ReplayStorage()
    {
        replayRecords = new();
    }
}
