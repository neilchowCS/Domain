using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BattleExecutor : MonoBehaviour
{
    public int execId;
    public bool isObserved = false;

    public UDListScriptableObject dataListSO;
    public List<IBattleStatus> battleStatuses;

    public MessengerReader reader;
    [SerializeField]
    protected RandomGenerators rngGen;
    public System.Random rng;

    public int globalTick;
    /// <summary>
    /// Holds delegates and events
    /// </summary>
    //public BattleEventHandler eventHandler;
    public EventManagement eventManager;
    public bool isInitializing;
    public EventLogger logger;

    public Factory factory;
    public ReplayManager replayManager;

    public Hexagon[,] hexMap;
    public List<List<MapTile>> mapTilesObj;
    public List<MapTile> mapTilesX0;
    public List<MapTile> mapTilesX1;
    public List<MapTile> mapTilesX2;
    public List<MapTile> mapTilesX3;
    public List<MapTile> mapTilesX4;
    public List<MapTile> mapTilesX5;
    public List<MapTile> mapTilesX6;
    public List<MapTile> mapTilesX7;
    public HexagonFunctions hexagonFunctions;

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

    public List<IBattleObject> playerObjects0;
    public List<IBattleObject> playerObjects1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Instantiate(AudioSingleton.PrefabAudio);
        ExecuteBattle();
    }

    public virtual void ExecuteBattle()
    {
        InitState(execId);
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

        eventManager.InvokeStartTurn();

        actingUnit.PerformAction();

        eventManager.InvokeEndTurn();

        eventManager.ClearEmptyTiles();

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
        isInitializing = true;
        //eventHandler = new BattleEventHandler(this);
        eventManager = new(this);
        logger = new(i);
        
        factory = new Factory(this);

        globalTick = 0;
        globalObjectId = 0;

        //timeline = new Timeline(this);
        hexMap = HexMapGenerator.GenerateHexMap();
        hexagonFunctions = new HexagonFunctions(hexMap.GetLength(0) - 1, hexMap.GetLength(1) - 1);
        mapTilesObj = new List<List<MapTile>> { mapTilesX0, mapTilesX1, mapTilesX2, mapTilesX3, mapTilesX4, mapTilesX5, mapTilesX6, mapTilesX7 };

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
        if (execId == 0)
        {
            rng = rngGen.rand0;
        }
        else
        {
            rng = rngGen.rand1;
        }

        InstantiateUnits();
        eventManager.OrderSpeedList();
        InitializeTimeline();

        isInitializing = false;
    }

    protected virtual void InstantiateUnits()
    {
        for (int i = 0; i < reader.record.team0Data.Count; i++)
        {
            if (reader.record.team0Data.Count <= reader.record.team0Position.Count)
            {
                player0Active.Add(factory.NewUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team0Data[i].unitId], reader.record.team0Data[i])),
                    hexagonFunctions.IndexToDimensional(reader.record.team0Position[i])));
            }
        }

        for (int i = 0; i < reader.record.team1Data.Count; i++)
        {
            if (reader.record.team1Data.Count <= reader.record.team1Position.Count)
            {
                player1Active.Add(factory.NewUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team1Data[i].unitId], reader.record.team1Data[i])),
                    hexagonFunctions.IndexToDimensional(reader.record.team1Position[i])));
            }
        }

        activeUnits = Enumerable.Concat(player0Active, player1Active).ToList();
    }

    public void InitializeTimeline()
    {
        if (activeUnits == null)
        {
            Debug.Log("ERROR: INSTANTIATE UNITS BEFORE TIMELINE");
            return;
        }

        Debug.Log(eventManager.orderedList.Count);
        float max = eventManager.orderedList[0].ObjSpeed.Value;

        foreach (IBattleUnit unit in activeUnits)
        {
            if (max != 0)
            {
                unit.Timeline = maxTimeline -
                    (unit.ObjSpeed.Value / max * maxTimeline);
            }
        }

        //DEBUG
        for (int i = 0; i < activeUnits.Count; i++)
        {
            logger.AddInitialize(activeUnits[i]);
        }
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

    public void EnqueueEvent(List<IEventCommand> command)
    {
        Debug.Log("event queue");
        eventManager.commandQueue.Enqueue(command);
        if (eventManager.commandQueue.Count <= 1)
        {
            Debug.Log("event call");
            eventManager.ExecuteQueue();
        }
    }

    public virtual void CreateDamageNumber(Vector3 unitPosition, int value, DamageType damageType, bool isCrit)
    {

    }

    public virtual void UpdateProfileDamage(int id, int amount)
    {

    }
}