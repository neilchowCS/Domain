using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BattleExecutor : MonoBehaviour
{
    public bool isObserved = false;

    public UDListScriptableObject dataListSO;

    public MessengerReader reader;

    public int globalTick;
    /// <summary>
    /// Holds delegates and events
    /// </summary>
    //public BattleEventHandler eventHandler;
    public EventManagement events;
    public EventLogger logger;

    public Factory factory;
    public ReplayManager replayManager;

    public List<MapVertex> mapGraph;
    public List<MapTile> mapTilesObj;

    public int maxTimeline = 100;
    //public float threshhold = 40;

    public IBattleUnit actingUnit;

    protected int globalObjectId;
    /// <summary>
    /// Sets global object ID and increments it.
    /// </summary>
    /// <returns></returns>
    public int SetGlobalObjectId()
    {
        return globalObjectId++;
    }

    public List<IBattleUnit> activeUnits;
    public List<IBattleUnit> player0Active;
    public List<IBattleUnit> player1Active;
    public List<IBattleUnit> player0Dead;
    public List<IBattleUnit> player1Dead;
    //[SerializeReference]
    public List<IBattleObject> playerObjects0;
    //[SerializeReference]
    public List<IBattleObject> playerObjects1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Instantiate(AudioSingleton.PrefabAudio);
        ExecuteBattle();
    }

    public virtual void ExecuteBattle()
    {
        InitState(0);
        //Debug.Log($"P0: {player0Active.Count}");
        //Debug.Log($"P1: {player1Active.Count}");
        globalTick++;
        while (ContinueRun())
        {
            StepUp();
        }
        Debug.Log(globalTick + " ticks");
        //timeline.Output();
        //Debug.Log(player1Active.Count == 0 && player0Active.Count >= 1 ? "Player won!" : "Player lost!");
        if (player1Active.Count == 0 && player0Active.Count >= 1)
        {
            logger.AddVictory(0);
            Debug.Log("Player won!");
            PlayerData data = DataSerialization.DeserializeStaticPlayerData(
                System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));
            if (reader.stageId > data.currentStage)
            {
                Debug.Log("Level Cleared! Campaign progressed!");
                data.currentStage = reader.stageId;
                string jsonOutput = DataSerialization.SerializeStaticPlayerData(data);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", jsonOutput);
            }
        }
        else
        {
            logger.AddVictory(1);
            Debug.Log("Player lost!");
        }
    }

    //Completely different fuction from observed
    public virtual void StepUp()
    {
        actingUnit = activeUnits[0];

        events.InvokeStartTurn();

        actingUnit.PerformAction();

        events.InvokeEndTurn();

        AdvanceTimeline();

        globalTick++;
    }

    public void AdvanceTimeline()
    {
        IBattleUnit next = null;

        foreach (IBattleUnit unit in activeUnits)
        {
            if (next == null || unit.Timeline / unit.UnitData.unitAttackSpeed.Value <
                next.Timeline / next.UnitData.unitAttackSpeed.Value)
            {
                next = unit;
            }
        }

        float distTime = next.Timeline / next.UnitData.unitAttackSpeed.Value;

        foreach (IBattleUnit unit in activeUnits)
        {
            logger.AddTimeline(unit, unit.Timeline, next.Timeline / next.UnitData.unitAttackSpeed.Value, unit.UnitData.unitAttackSpeed.Value * (next.Timeline / next.UnitData.unitAttackSpeed.Value), unit.Timeline - unit.UnitData.unitAttackSpeed.Value * (next.Timeline / next.UnitData.unitAttackSpeed.Value), (unit == next));
        }

        foreach (IBattleUnit unit in activeUnits)
        {
            unit.Timeline -= unit.UnitData.unitAttackSpeed.Value * distTime;
        }

        activeUnits = activeUnits.OrderBy(o => o.Timeline).ToList();
    }

    public bool ContinueRun()
    {
        return (player0Active.Count > 0 && player1Active.Count > 0 && globalTick < 4000);
    }

    protected virtual void InitState(int i)
    {
        //eventHandler = new BattleEventHandler(this);
        events = new(this);
        logger = new(i);
        factory = new Factory(this);

        globalTick = 0;
        globalObjectId = 0;

        //timeline = new Timeline(this);
        mapGraph = MapGraph.GetMap();

        player0Active = new List<IBattleUnit>();
        player0Dead = new List<IBattleUnit>();
        playerObjects0 = new List<IBattleObject>();

        player1Active = new List<IBattleUnit>();
        player1Dead = new List<IBattleUnit>();
        playerObjects1 = new List<IBattleObject>();

        if (!reader.hasRead)
        {
            reader.ReadTeamMessenger();
        }

        InstantiateUnits();
        events.Initialize(activeUnits);
    }

    protected virtual void InstantiateUnits()
    {
        for (int i = 0; i < reader.record.team0Data.Count; i++)
        {
            if (reader.record.team0Data.Count <= reader.record.team0Position.Count)
            {
                player0Active.Add(factory.NewUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team0Data[i].unitId], reader.record.team0Data[i])),
                    reader.record.team0Position[i]));
            }
        }

        for (int i = 0; i < reader.record.team1Data.Count; i++)
        {
            if (reader.record.team1Data.Count <= reader.record.team1Position.Count)
            {
                player1Active.Add(factory.NewUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team1Data[i].unitId], reader.record.team1Data[i])),
                    reader.record.team1Position[i]));
            }
        }

        activeUnits = Enumerable.Concat(player0Active, player1Active).ToList();
        activeUnits = activeUnits.OrderByDescending(o => o.UnitData.unitSpeed.Value).ToList();
        InitializeTimeline();
    }

    public void InitializeTimeline()
    {
        if (activeUnits == null)
        {
            Debug.Log("ERROR: INSTANTIATE UNITS BEFORE TIMELINE");
            return;
        }

        float max = activeUnits[0].UnitData.unitSpeed.Value;

        foreach (IBattleUnit unit in activeUnits)
        {
            if (max != 0)
            {
                unit.Timeline = maxTimeline -
                    (unit.UnitData.unitSpeed.Value / max * maxTimeline);
            }
        }

        //DEBUG
        for (int i = 0; i < activeUnits.Count; i++)
        {
            logger.AddInitialize(activeUnits[i]);
        }
    }

    /*
    protected void CleanUp()
    {
        foreach (IBattleUnit unit in player0Dead)
        {
            if (unit.NeedsCleaning)
            {
                //eventHandler.TickUp -= unit.Behavior.OnTickUp;
                //subscribe to onUnitDeadTick
                unit.NeedsCleaning = false;
            }
        }
        foreach (IBattleUnit unit in player1Dead)
        {
            if (unit.NeedsCleaning)
            {
                //eventHandler.TickUp -= unit.Behavior.OnTickUp;
                unit.NeedsCleaning = false;
            }
        }
    }
    */

    public void RemoveObject(IBattleObject obj)
    {
        //eventHandler.TickUp -= obj.Behavior.OnTickUp;
        playerObjects0.Remove(obj);
        playerObjects1.Remove(obj);
    }

    public List<IBattleObject> GetAlliedObjects(IBattleObject obj)
    {
        if (obj.Side == 0)
        {
            return playerObjects0;
        }
        return playerObjects1;
    }

    public List<IBattleObject> GetEnemyObjects(IBattleObject obj)
    {
        if (obj.Side == 0)
        {
            return playerObjects1;
        }
        return playerObjects0;
    }

    public List<IBattleUnit> GetAlliedUnits(IBattleObject obj)
    {
        if (obj.Side == 0)
        {
            return player0Active;
        }
        return player1Active;
    }

    public List<IBattleUnit> GetEnemyUnits(IBattleObject obj)
    {
        if (obj.Side == 0)
        {
            return player1Active;
        }
        return player0Active;
    }

    public void EnqueueEvent(IEventCommand command)
    {
        events.commandQueue.Enqueue(command);
        if (events.commandQueue.Count <= 1)
        {
            events.ExecuteQueue();
        }
    }

    public virtual void CreateDamageNumber(Vector3 unitPosition, int value, DamageType damageType)
    {

    }

    public virtual void UpdateProfileDamage(int id, int amount)
    {

    }
}