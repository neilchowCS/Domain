using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public ResourceHandler resourceHandler;
    public TextMeshProUGUI stageRewardText;
    public TextMeshProUGUI availableText;

    public System.DateTime dateTime;

    private const float baseGPM = 12.5f;
    private const float baseEPM = 19;

    private float goldPerMin;
    public float GoldPerMin { get { return goldPerMin; }
        set { goldPerMin = value; DisplayStageReward(); } }

    public float essencePerMin;
    public float EssencePerMin
    {
        get { return essencePerMin; }
        set { essencePerMin = value; DisplayStageReward(); }
    }

    float elapsed = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        //FIXME
        //DEPENDENT ON HANDLER RUNNING BEFORE
        SetStageReward();
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

        resourceHandler.ChangeGoldAmount(reward.Item1);
        resourceHandler.ChangeEssenceAmount(reward.Item2);
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

    public void SetStageReward()
    {
        float coefficient = .5f;
        GoldPerMin = baseGPM + baseGPM * (resourceHandler.playerData.currentStage + 1) * coefficient;
        EssencePerMin = baseEPM + baseEPM * (resourceHandler.playerData.currentStage + 1) * coefficient;
    }

    public void DisplayStageReward()
    {
        /*
        Gold/Min: 12.5
        Essence/Min: 19
        */

        stageRewardText.text = $"Gold/Min: {GoldPerMin}\nEssence/Min: {EssencePerMin}\nCurrent Stage {resourceHandler.playerData.currentStage}";
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
