using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    /*
    when a battle is won, identify what reward is given
    2 kinds: first victory, subsequent clears
    credit proper reward when victory is identified.
    handles smash
    */

    /*

    have separate scriptable object of reward data
    Items should be identified by string or int id???


    */


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetRewards(int stageId)
    {
        PlayerData playerData = DataSerialization.DeserializeStaticPlayerData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));

        if (stageId >= 0 && stageId < 100)
        {
            playerData.playerInventory[0] += 0;
        }
        else
        {
            switch (stageId) { }
        }

        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json",
                DataSerialization.SerializeStaticPlayerData(playerData));
    }

    private void CalcCampaignVictoryReward(int stageId)
    {

    }
}