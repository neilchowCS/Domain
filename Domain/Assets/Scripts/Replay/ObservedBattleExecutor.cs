using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedBattleExecutor : BattleExecutor
{
    public List<ReplayProfile> profiles;
    public List<ReplayProfile> side0Profiles;
    public List<ReplayProfile> side1Profiles;
    public List<Vector3> side0ProfilePositions;
    public List<Vector3> side1ProfilePositions;

    public override void Start()
    {
        //Instantiate(AudioSingleton.PrefabAudio);
        //ExecuteBattle();
        InitState();
        Debug.Log($"P0: {player0Active.Count}");
        Debug.Log($"P1: {player1Active.Count}");
        globalTick++;
    }

    public override void ExecuteBattle()
    {
        if (this.enabled == false)
        {
            InitState();
            
            Debug.Log($"P0: {player0Active.Count}");
            Debug.Log($"P1: {player1Active.Count}");
            globalTick++;
            this.enabled = true;
        }
    }

    protected override void InitState()
    {
        base.InitState();

        profiles = new();
        side0Profiles = new();
        side1Profiles = new();
        side0ProfilePositions = new();
        side1ProfilePositions = new();

        foreach (IBattleUnit unit in player0Active)
        {
            InitProfile(unit);
        }
        foreach (IBattleUnit unit in player1Active)
        {
            InitProfile(unit);
        }
    }

    private void FixedUpdate()
    {
        if (ContinueRun())
        {
            StepUp();
        }
        else
        {
            Debug.Log(globalTick + " ticks");
            Debug.Log(player1Active.Count == 0 && player0Active.Count >= 1 ? "Player won!" : "Player lost!");
            this.enabled = false;
        }
    }

    protected override void InstantiateUnits()
    {
        for (int i = 0; i < reader.record.team0Data.Count; i++)
        {
            if (reader.record.team0Data.Count <= reader.record.team0Position.Count)
            {
                player0Active.Add(factory.NewObservedUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team0Data[i].unitId], reader.record.team0Data[i])),
                    reader.record.team0Position[i]));
            }
        }

        for (int i = 0; i < reader.record.team1Data.Count; i++)
        {
            if (reader.record.team1Data.Count <= reader.record.team1Position.Count)
            {
                player1Active.Add(factory.NewObservedUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team1Data[i].unitId], reader.record.team1Data[i])),
                    reader.record.team1Position[i]));
            }
        }
    }

    public void InitProfile(IBattleUnit unit)
    {
        ReplayProfile y = Instantiate(replayManager.profile).GetComponent<ReplayProfile>();
        profiles.Add(y);
        y.globalId = unit.GlobalObjectId;
        y.SetName(unit.ObjectName);
        y.SetImage(unit.UnitData.baseData.unitSprite);
        if (unit.Side == 0)
        {
            y.transform.SetParent(replayManager.leftContent.transform, false);
            side0Profiles.Add(y);
            /*y.transform.localPosition = new Vector3(y.transform.localPosition.x,
                y.transform.localPosition.y - (side0ProfilePositions.Count) * 75,
                y.transform.localPosition.z);*/
            side0ProfilePositions.Add(y.transform.position);
        }
        else
        {
            y.transform.SetParent(replayManager.rightContent.transform, false);
            side1Profiles.Add(y);
            /*y.transform.localPosition = new Vector3(-y.transform.localPosition.x,
                y.transform.localPosition.y - (side1ProfilePositions.Count) * 75,
                y.transform.localPosition.z);*/
            side1ProfilePositions.Add(y.transform.position);
        }
    }

    public void UpdateProfileDamage(int id, int amount)
    {
        foreach (ReplayProfile i in profiles)
        {
            if (i.globalId == id)
            {
                i.IncreaseDamage(amount);
                break;
            }
        }

        //replayExecutor.CreateDamageNumber(target.transform.position, amount, damageType);
        ReorderProfile();
    }

    public void ReorderProfile()
    {
        side0Profiles = side0Profiles.OrderByDescending(o => o.damageInt).ToList();
        side1Profiles = side1Profiles.OrderByDescending(o => o.damageInt).ToList();
        int side0DamageSum = side0Profiles[0].damageInt;
        int side1DamageSum = side1Profiles[0].damageInt;
        /*
        foreach (ReplayProfile i in profiles)
        {
            SetProfilePosition(i);
            SetBars(i, side0DamageSum, side1DamageSum);
        }
        */

        for (int i = 0; i < side0Profiles.Count; i++)
        {
            side0Profiles[i].transform.SetSiblingIndex(i);
            SetBars(side0Profiles[i], side0DamageSum, side1DamageSum);
        }

        for (int i = 0; i < side1Profiles.Count; i++)
        {
            side1Profiles[i].transform.SetSiblingIndex(i);
            SetBars(side1Profiles[i], side0DamageSum, side1DamageSum);
        }
    }

    public void SetBars(ReplayProfile replayProfile, int side0Sum, int side1Sum)
    {
        if (replayProfile.transform.localPosition.x < 0)
        {
            replayProfile.bar.fillAmount = replayProfile.damageInt / (float)side0Sum;
        }
        else
        {
            replayProfile.bar.fillAmount = replayProfile.damageInt / (float)side1Sum;
        }
    }
}
