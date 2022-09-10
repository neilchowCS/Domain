using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public ResourceHandler resourceHandler;
    public TextMeshProUGUI availableText;

    public System.DateTime dateTime;
    private float goldPerMin = 12.5f;
    private float essencePerMin = 19;

    float elapsed = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        DisplayAvailableReward();
    }

    // replace with coroutine
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= .25f)
        {
            elapsed = 0f;
            DisplayAvailableReward();
        }
    }

    public void ClaimReward()
    {
        (int, int, int, float, float) reward = CalcReward();

        resourceHandler.playerData.gold += reward.Item1;
        resourceHandler.playerData.essence += reward.Item2;
        resourceHandler.playerData.lastClaim = (System.DateTime.UtcNow.AddSeconds(-reward.Item3)).ToString();
        resourceHandler.playerData.overflowGold = reward.Item4;
        resourceHandler.playerData.overflowEssence = reward.Item5;
        resourceHandler.WritePlayerData();

        DisplayAvailableReward();
    }

    public (int, int, int, float, float) CalcReward()
    {
        System.TimeSpan timeSpan = (System.DateTime.UtcNow -
            System.DateTime.Parse(resourceHandler.playerData.lastClaim));
        int numMin = Mathf.Min((int)(timeSpan.TotalMinutes), 480);
        int overflow = 0;
        if (numMin != 480)
        {
            overflow = timeSpan.Seconds;
        }

        float outputGold = (numMin * goldPerMin) + resourceHandler.playerData.overflowGold;
        float outputEssence = (numMin * essencePerMin) + resourceHandler.playerData.overflowEssence;

        float overflowGold = outputGold % 1;
        float overflowEssence = outputEssence % 1;

        //Debug.Log(overflowGold);

        return ((int)(outputGold), (int)(outputEssence), overflow, overflowGold, overflowEssence);
    }

    public void DisplayAvailableReward()
    {
        (int, int, int, float, float) reward = CalcReward();

        availableText.text = $"Gold: {reward.Item1}\nEssence: {reward.Item2}";
    }

    public void DisplayAvailableReward((int, int, int, float, float) prevCalc)
    {
        availableText.text = $"Gold: {prevCalc.Item1}\nEssence: {prevCalc.Item2}";
    }
}
