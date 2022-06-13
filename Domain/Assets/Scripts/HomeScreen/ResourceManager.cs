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

    float elapsed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            DisplayAvailableReward();
        }
    }

    public void ClaimReward()
    {
        (int, int, int) reward = CalcReward();

        resourceHandler.playerData.gold += reward.Item1;
        resourceHandler.playerData.essence += reward.Item2;
        resourceHandler.playerData.lastClaim = (System.DateTime.UtcNow.AddSeconds(-reward.Item3)).ToString();
        resourceHandler.WritePlayerData();

        DisplayAvailableReward();
    }

    public (int, int, int) CalcReward()
    {
        System.TimeSpan timeSpan = (System.DateTime.UtcNow -
            System.DateTime.Parse(resourceHandler.playerData.lastClaim));
        int numMin = Mathf.Min((int)(timeSpan.TotalMinutes), 480);
        int overflow = 0;
        if (numMin != 480)
        {
            overflow = timeSpan.Seconds;
        }
        return ((int)(numMin * goldPerMin), (int)(numMin * essencePerMin), overflow);
    }

    public void DisplayAvailableReward()
    {
        (int, int, int) reward = CalcReward();

        availableText.text = $"Gold: {reward.Item1}\nEssence: {reward.Item2}";
    }

    public void DisplayAvailableReward((int, int, int) prevCalc)
    {
        availableText.text = $"Gold: {prevCalc.Item1}\nEssence: {prevCalc.Item2}";
    }
}
