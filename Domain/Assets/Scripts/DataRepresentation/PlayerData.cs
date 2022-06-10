using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int essence;
    public int promotionStones;

    public PlayerData()
    {

    }

    public PlayerData(int i, int j, int k)
    {
        gold = i;
        essence = j;
        promotionStones = k;
    }
}
