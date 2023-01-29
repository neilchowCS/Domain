using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageRewardInstance
{
    public int itemId;
    public int countMin;
    public int countMax;
    public float chance;
    //0: even distribution, ignore chance
    public int randomMode;

    public StageRewardInstance(int itemId, int countMin, int countMax, float chance, int randomMode)
    {
        this.itemId = itemId;
        this.countMin = countMin;
        this.countMax = countMax;
        this.chance = chance;
        this.randomMode = randomMode;
    }
}
