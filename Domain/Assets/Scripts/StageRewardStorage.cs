using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageRewardStorage
{
    [SerializeField]
    public List<StageInstanceReward> stageInstances;

    public StageRewardStorage()
    {
        stageInstances = new();
    }
}
