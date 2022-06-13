using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int essence;
    public float overflowGold;
    public float overflowEssence;
    public int promotionStones;
    public string lastClaim;

    public PlayerData()
    {

    }

    public PlayerData(int i, int j, int k, string lastClaim, float ovfG, float ovfE)
    {
        gold = i;
        essence = j;
        overflowGold = ovfE;
        overflowEssence = ovfG;
        promotionStones = k;
        this.lastClaim = lastClaim;
    }
}
