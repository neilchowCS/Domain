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

    public TimelineUI timelineUI;

    private float timer = 0;

    private bool bottlenecked = false;

    public override void Start()
    {
        //Instantiate(AudioSingleton.PrefabAudio);
        //ExecuteBattle();
        InitState(1);
        //Debug.Log($"P0: {player0Active.Count}");
        //Debug.Log($"P1: {player1Active.Count}");
        globalTick++;
    }

    public override void ExecuteBattle()
    {
        if (this.enabled == false)
        {
            InitState(1);
            
            Debug.Log($"P0: {player0Active.Count}");
            Debug.Log($"P1: {player1Active.Count}");
            globalTick++;
            this.enabled = true;
        }
    }

    protected override void InitState(int i)
    {
        base.InitState(i);

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

        timelineUI.InitTimeline(activeUnits);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !bottlenecked)
        {
            timer = 0;
            if (ContinueRun())
            {
                BeginTurnCycle();
            }
            else
            {
                Debug.Log(globalTick + " ticks");
                //Debug.Log(player1Active.Count == 0 && player0Active.Count >= 1 ? "Player won!" : "Player lost!");
                if (player1Active.Count == 0 && player0Active.Count >= 1)
                {
                    logger.AddVictory(0);
                    Debug.Log("Player won!");
                }
                else
                {
                    logger.AddVictory(1);
                    Debug.Log("Player lost!");
                }
                this.enabled = false;
            }
        }
    }

    public void BeginTurnCycle()
    {
        actingUnit = activeUnits[0];

        events.InvokeStartTurn();

        actingUnit.PerformAction();

        timer += .7f;

        bottlenecked = true;
    }

    public override void StepUp()
    {
        bottlenecked = false;

        events.InvokeEndTurn();

        AdvanceTimeline();

        timelineUI.RefreshTimeline();

        globalTick++;
    }

    protected override void InstantiateUnits()
    {
        for (int i = 0; i < reader.record.team0Data.Count; i++)
        {
            if (reader.record.team0Data.Count <= reader.record.team0Position.Count)
            {
                player0Active.Add(factory.NewObservedUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team0Data[i].unitId], reader.record.team0Data[i])),
                    hexagonFunctions.IndexToDimensional(reader.record.team0Position[i])));
            }
        }

        for (int i = 0; i < reader.record.team1Data.Count; i++)
        {
            if (reader.record.team1Data.Count <= reader.record.team1Position.Count)
            {
                player1Active.Add(factory.NewObservedUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team1Data[i].unitId], reader.record.team1Data[i])),
                    hexagonFunctions.IndexToDimensional(reader.record.team1Position[i])));
            }
        }

        activeUnits = Enumerable.Concat(player0Active, player1Active).ToList();
        activeUnits = activeUnits.OrderByDescending(o => o.UnitData.unitSpeed.Value).ToList();
        InitializeTimeline();
    }

    public void InitProfile(IBattleUnit unit)
    {
        ReplayProfile y = Instantiate(replayManager.profile).GetComponent<ReplayProfile>();
        profiles.Add(y);
        //Debug.Log(unit.GlobalObjectId);
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

    public override void UpdateProfileDamage(int id, int amount)
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

    protected void ReorderProfile()
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
            side0Profiles[i].bar.fillAmount = side0Profiles[i].damageInt / (float)side0DamageSum;
        }

        for (int i = 0; i < side1Profiles.Count; i++)
        {
            side1Profiles[i].transform.SetSiblingIndex(i);
            side1Profiles[i].bar.fillAmount = side1Profiles[i].damageInt / (float)side1DamageSum;
        }
    }

    public override void CreateDamageNumber(Vector3 unitPosition, int value, DamageType damageType)
    {
        DamageNumber x = Instantiate(replayManager.damageNumber,
                replayManager.screenOverlayCanvas.transform, false);
        x.transform.position = Camera.main.WorldToScreenPoint(unitPosition) + new Vector3(0, 50, 0);


        if (damageType == DamageType.normal)
        {
            x.textMesh.text = "-" + value;
        }
        else if (damageType == DamageType.special)
        {
            x.textMesh.text = "-" + value;
            x.textMesh.color = new Color32(143, 0, 254, 255);
        }
        else if (damageType == DamageType.healing)
        {
            x.textMesh.text = "+" + value;
            x.textMesh.color = Color.green;
        }
    }
}
