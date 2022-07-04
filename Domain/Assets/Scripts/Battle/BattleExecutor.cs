using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleExecutor : MonoBehaviour
{
    public static bool isObserved;

    public ReplayExecutor replayExecutor;

    public UDListScriptableObject dataListSO;

    public TeamData team0;
    public int stageId = -1;
    public TeamData team1;

    public int globalTick;
    /// <summary>
    /// Holds delegates and events
    /// </summary>
    public BattleEventHandler eventHandler;

    public BattleSpace battleSpace;

    private int globalObjectId;
    /// <summary>
    /// Sets global object ID and increments it.
    /// </summary>
    /// <returns></returns>
    public int SetGlobalObjectId()
    {
        globalObjectId++;
        return (globalObjectId - 1);
    }

    public List<BattleUnit> player0;
    public List<BattleUnit> player1;
    public List<BattleUnit> player0Dead;
    public List<BattleUnit> player1Dead;
    public List<BattleObject> playerObjects0;
    public List<BattleObject> playerObjects1;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(AudioSingleton.PrefabAudio);
        ExecuteBattle();
    }

    public void ExecuteBattle()
    {
        InitState();

        globalTick++;
        while (ContinueRun())
        {
            StepUp();
        }
        //timeline.Output();
        Debug.Log(player0.Count != 0 ? "Player won!" : "Player lost!");
        replayExecutor.StartReplay();//timeline);
    }

    public void StepUp()
    {
        eventHandler.OnTickUp();
        CleanUp();
    }

    public bool ContinueRun()
    {
        return (player0.Count > 0 && player1.Count > 0 && globalTick < 4000);
    }

    /*
    public bool IsRunning(int i)
    {
        if (i == 0)
        {
            return player1.Count > 0;
        }else if (i == 1)
        {
            return player0.Count > 0;
        }
        return false;
    }
    */

    private void InitState()
    {
        eventHandler = new BattleEventHandler(this);

        globalTick = 0;
        globalObjectId = 0;

        //timeline = new Timeline(this);
        battleSpace = new BattleSpace();

        player0 = new List<BattleUnit>();
        player0Dead = new List<BattleUnit>();
        playerObjects0 = new List<BattleObject>();

        player1 = new List<BattleUnit>();
        player1Dead = new List<BattleUnit>();
        playerObjects1 = new List<BattleObject>();

        //Create TeamData
        if (!ReadTeamMessenger())
        {
            if (team0 != null)
            {
                team0.RefreshRuntimeData();
            }
            else
            {
                team0 = new TeamData();
            }
        }
        team1 = new TeamData();

        //Generate player runtime data
        for (int i = 0; i < team0.unitList.Count; i++)
        {
            if (team0.unitList.Count == team0.positionList.Count)
            {
                player0.Add(BattleUnitConstructor.GetBattleUnit(this, 0,
                    team0.unitList[i], team0.positionList[i]));
            }
            else
            {
                player0.Add(BattleUnitConstructor.GetBattleUnit(this, 0, team0.unitList[i]));
            }
        }

        //Read stage data, generate enemy runtime data
        //FIXME
        DataSerialization serializer = new DataSerialization();
        StageDataCollection stageData = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));
        team1 = new TeamData(stageData.stageDataList[stageId], dataListSO);

        for (int i = 0; i < team1.unitList.Count; i++)
        {
            if (team1.unitList.Count == team1.positionList.Count)
            {
                player1.Add(BattleUnitConstructor.GetBattleUnit(this, 1,
                    team1.unitList[i], team1.positionList[i]));
            }
            else
            {
                player1.Add(BattleUnitConstructor.GetBattleUnit(this, 1, team1.unitList[i]));
            }
        }

        ReplayStorage storage = DataSerialization.DeserializeReplayStore(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/ReplayRecord.json"));
        if (storage == null)
        {
            storage = new ReplayStorage();
        }

        PrimitiveTeamData temp = new PrimitiveTeamData();
        foreach (UnitRuntimeData i in team0.unitList)
        {
            temp.dataList.Add(i.individualData);
        }
        temp.positionList = team0.positionList;
        storage.team1List.Add(temp);

        PrimitiveTeamData temp1 = new PrimitiveTeamData();
        foreach (UnitRuntimeData i in team1.unitList)
        {
            temp1.dataList.Add(i.individualData);
        }
        temp1.positionList = team1.positionList;
        storage.team2List.Add(temp1);

        storage.seedList.Add(UnityEngine.Random.state);

        string jsonOutput = DataSerialization.SerializeData(storage);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ReplayRecord.json", jsonOutput);
    }

    /// <summary>
    /// Reads team messenger and sets team0, stage id. 
    /// Returns false if no messenger found
    /// </summary>
    private bool ReadTeamMessenger()
    {
        TeamMessenger m;
        m = FindObjectOfType<TeamMessenger>();
        if (m != null)
        {
            team0 = m.teamData;
            stageId = m.stageId;
            foreach (TeamMessenger messenger in FindObjectsOfType<TeamMessenger>())
            {
                Destroy(messenger.gameObject);
            }
            return true;
        }
        return false;
    }

    private void CleanUp()
    {
        foreach (BattleUnit unit in player0Dead)
        {
            if (unit.needsCleaning)
            {
                eventHandler.TickUp -= unit.OnTickUp;
                unit.needsCleaning = false;
            }
        }
        foreach (BattleUnit unit in player1Dead)
        {
            if (unit.needsCleaning)
            {
                eventHandler.TickUp -= unit.OnTickUp;
                unit.needsCleaning = false;
            }
        }
    }

    public void RemoveObject(BattleObject obj)
    {
        eventHandler.TickUp -= obj.OnTickUp;
        playerObjects0.Remove(obj);
        playerObjects1.Remove(obj);
    }

    public List<BattleObject> GetAlliedObjects(BattleObject obj)
    {
        if (obj.side == 0)
        {
            return playerObjects0;
        }
        return playerObjects1;
    }

    public List<BattleObject> GetEnemyObjects(BattleObject obj)
    {
        if (obj.side == 0)
        {
            return playerObjects1;
        }
        return playerObjects0;
    }

    public List<BattleUnit> GetAlliedUnits(BattleObject obj)
    {
        if (obj.side == 0)
        {
            return player0;
        }
        return player1;
    }

    public List<BattleUnit> GetEnemyUnits(BattleObject obj)
    {
        if (obj.side == 0)
        {
            return player1;
        }
        return player0;
    }

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public void DealDamage(BattleUnit damageSource, BattleUnit damageTarget,
        int amount, DamageType damageType)
    {
        if (damageSource == null)
        {
            Debug.Log("damage error!");
        }

        if (damageType == DamageType.normal)
        {
            if (damageTarget.unitData.armorReduction < 1)
            {
                amount = (int)(amount * damageTarget.unitData.armorReduction);
            }
        }

        //damage reduction calcs here
        eventHandler.OnDamageDealt(damageSource, damageTarget, amount);
    }

    public void ApplyHeal(BattleUnit healSource, BattleUnit healTarget, int amount)
    {
        Debug.Log(amount);
        //apply healing reduction before
        if (healTarget.unitData.health + amount > healTarget.unitData.unitMaxHealth.Value)
        {
            amount = healTarget.unitData.unitMaxHealth.Value - healTarget.unitData.health;
        }

        if (healSource == null)
        {
            Debug.Log("healing error!");
        }

        eventHandler.OnHealApplied(healSource, healTarget, amount);
    }
}