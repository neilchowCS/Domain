using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInstanceReward
{
    public List<StageRewardInstance> rewardInstances;

    public StageInstanceReward(List<StageRewardInstance> rewardInstances)
    {
        this.rewardInstances = rewardInstances;
    }

    public StageInstanceReward()
    {
        this.rewardInstances = new();
    }
}
