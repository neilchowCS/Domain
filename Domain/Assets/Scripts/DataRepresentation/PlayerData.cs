using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerId;
    public int gold;
    public int essence;
    public float overflowGold;
    public float overflowEssence;
    public int promotionStones;
    public string lastClaim;

    public int currentStage;

    public PlayerData()
    {

    }

    public PlayerData(int playerId, int gold, int essence, int promoStones, string lastClaim,
        float ovfG, float ovfE, int cStage)
    {
        this.playerId = playerId;
        this.gold = gold;
        this.essence = essence;
        promotionStones = promoStones;

        this.lastClaim = lastClaim;

        overflowGold = ovfE;
        overflowEssence = ovfG;

        currentStage = cStage;
    }
}
