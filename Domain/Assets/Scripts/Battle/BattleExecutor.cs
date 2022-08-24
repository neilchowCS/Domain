using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleExecutor : MonoBehaviour
{
    public bool isObserved = false;

    public UDListScriptableObject dataListSO;

    public BattleRecord record;
    private bool replayFlag = false;

    public int globalTick;
    /// <summary>
    /// Holds delegates and events
    /// </summary>
    public BattleEventHandler eventHandler;
    public Factory factory;
    public ReplayManager replayManager;

    public BattleSpace battleSpace;

    protected int globalObjectId;
    /// <summary>
    /// Sets global object ID and increments it.
    /// </summary>
    /// <returns></returns>
    public int SetGlobalObjectId()
    {
        return globalObjectId++;
    }

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
        InitState();
        Debug.Log($"P0: {player0Active.Count}");
        Debug.Log($"P1: {player1Active.Count}");
        globalTick++;
        while (ContinueRun())
        {
            StepUp();
        }
        Debug.Log(globalTick + " ticks");
        //timeline.Output();
        Debug.Log(player1Active.Count == 0 && player0Active.Count >= 1 ? "Player won!" : "Player lost!");
    }

    public void StepUp()
    {
        eventHandler.OnTickUp();
        CleanUp();
    }

    public bool ContinueRun()
    {
        return (player0Active.Count > 0 && player1Active.Count > 0 && globalTick < 4000);
    }

    protected void InitState()
    {
        eventHandler = new BattleEventHandler(this);
        factory = new Factory(this);

        globalTick = 0;
        globalObjectId = 0;

        //timeline = new Timeline(this);
        battleSpace = new BattleSpace();

        player0Active = new List<IBattleUnit>();
        player0Dead = new List<IBattleUnit>();
        playerObjects0 = new List<IBattleObject>();

        player1Active = new List<IBattleUnit>();
        player1Dead = new List<IBattleUnit>();
        playerObjects1 = new List<IBattleObject>();

        if (!ReadTeamMessenger())
        {
            if (record == null)
            {
                record = new BattleRecord();
            }
        }
        else if (replayFlag == false)
        {
            ReplayStorage storage = DataSerialization.DeserializeReplayStore(
                System.IO.File.ReadAllText(Application.persistentDataPath + "/ReplayRecord.json"));
            if (storage == null)
            {
                storage = new ReplayStorage();
            }
            else if (storage.replayRecords.Count >= 20)
            {
                storage.replayRecords.RemoveAt(0);
            }

            storage.replayRecords.Add(new ReplayRecord(record, UnityEngine.Random.state));

            string jsonOutput = DataSerialization.SerializeData(storage);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/ReplayRecord.json", jsonOutput);
        }

        InstantiateUnits();
    }

    protected virtual void InstantiateUnits()
    {
        for (int i = 0; i < record.team0Data.Count; i++)
        {
            if (record.team0Data.Count <= record.team0Position.Count)
            {
                player0Active.Add(factory.NewUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[record.team0Data[i].unitId], record.team0Data[i])),
                    record.team0Position[i]));
            }
        }

        for (int i = 0; i < record.team1Data.Count; i++)
        {
            if (record.team1Data.Count <= record.team1Position.Count)
            {
                player1Active.Add(factory.NewUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[record.team1Data[i].unitId], record.team1Data[i])),
                    record.team1Position[i]));
            }
        }
    }

    /// <summary>
    /// Reads team messenger and sets team0, stage id. 
    /// Returns false if no messenger found
    /// </summary>
    protected bool ReadTeamMessenger()
    {
        TeamMessenger m = FindObjectOfType<TeamMessenger>();
        if (m)
        {
            Debug.Log("battle");
            record = m.teamRecord;
            foreach (TeamMessenger messenger in FindObjectsOfType<TeamMessenger>())
            {
                Destroy(messenger.gameObject);
            }
            return true;
        }

        ReplayMessenger r = FindObjectOfType<ReplayMessenger>();
        if (r)
        {
            Debug.Log("replay");
            replayFlag = true;
            record = new BattleRecord(r.record);
            UnityEngine.Random.state = r.record.seed;
            return true;
        }

        return false;
    }

    protected void CleanUp()
    {
        foreach (IBattleUnit unit in player0Dead)
        {
            if (unit.NeedsCleaning)
            {
                eventHandler.TickUp -= unit.Behavior.OnTickUp;
                //subscribe to onUnitDeadTick
                unit.NeedsCleaning = false;
            }
        }
        foreach (IBattleUnit unit in player1Dead)
        {
            if (unit.NeedsCleaning)
            {
                eventHandler.TickUp -= unit.Behavior.OnTickUp;
                unit.NeedsCleaning = false;
            }
        }
    }

    public void RemoveObject(IBattleObject obj)
    {
        eventHandler.TickUp -= obj.Behavior.OnTickUp;
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

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public void DealDamage(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType)
    {
        if (damageSource == null)
        {
            Debug.Log("damage error!");
        }

        if (damageType == DamageType.normal)
        {
            if (damageTarget.UnitData.armorReduction < 1)
            {
                amount = (int)(amount * damageTarget.UnitData.armorReduction);
            }
        }

        //damage reduction calcs here
        eventHandler.OnDamageDealt(damageSource, damageTarget, amount);
    }

    public void ApplyHeal(IBattleUnit healSource, IBattleUnit healTarget, int amount)
    {
        Debug.Log(amount);
        //apply healing reduction before
        if (healTarget.UnitData.health + amount > healTarget.UnitData.unitMaxHealth.Value)
        {
            amount = healTarget.UnitData.unitMaxHealth.Value - healTarget.UnitData.health;
        }

        if (healSource == null)
        {
            Debug.Log("healing error!");
        }

        eventHandler.OnHealApplied(healSource, healTarget, amount);
    }
}