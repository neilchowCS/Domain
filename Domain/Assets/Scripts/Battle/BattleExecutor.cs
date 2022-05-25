using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleExecutor : MonoBehaviour
{
    public bool run = true;
    public ReplayExecutor replayExecutor;

    public Timeline timeline;
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

    public List<BattleUnit>[] playerUnits;
    public List<BattleObject>[] playerObjects;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(AudioSingleton.PrefabAudio);
        ExecuteBattle();
    }

    public void ExecuteBattle()
    {
        run = true;
        InitState();

        globalTick++;
        while (run)
        {
            StepUp();
        }
        //timeline.Output();
    }

    public void StepUp()
    {
        if (IsRunning())
        {
            eventHandler.OnTickUp();
            CleanUp();
        }
        else
        {
            Debug.Log(player0.Count != 0 ? "Player won!" : "Player lost!");
            run = false;
            timeline.AddTimelineEvent(new TimelineEnd());
            replayExecutor.StartReplay(timeline);
        }
        if (globalTick > 4000)
        {
            Debug.Log("Player timed out!");
            run = false;
        }
    }

    public bool IsRunning()
    {
        return player0.Count > 0 && player1.Count > 0;
    }

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

    private void InitState()
    {
        eventHandler = new BattleEventHandler(this);

        globalTick = 0;
        globalObjectId = 0;

        timeline = new Timeline(this);
        battleSpace = new BattleSpace();

        player0 = new List<BattleUnit>();
        player0Dead = new List<BattleUnit>();
        playerObjects0 = new List<BattleObject>();

        player1 = new List<BattleUnit>();
        player1Dead = new List<BattleUnit>();
        playerObjects1 = new List<BattleObject>();

        playerUnits = new List<BattleUnit>[] { player0, player0Dead, player1, player1Dead };
        playerObjects = new List<BattleObject>[] { playerObjects0, playerObjects1 };

        if (!ReadTeamMessenger())
        {
            team0 = new TeamData();
        }
        team1 = new TeamData();

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
    }

    /// <summary>
    /// returns false if no messenger found
    /// </summary>
    private bool ReadTeamMessenger()
    {
        TeamMessenger m;
        m = FindObjectOfType<TeamMessenger>();
        if (m != null)
        {
            team0 = m.teamData;
            stageId = m.stageId;
            foreach(TeamMessenger messenger in FindObjectsOfType<TeamMessenger>())
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
    public void DealDamage(BattleUnit damageSource, BattleUnit damageTarget, int amount)
    {
        eventHandler.OnDamageDealt(damageSource, damageTarget, amount);
        if (damageSource == null)
        {
            Debug.Log("error!");
        }
        timeline.AddTimelineEvent(new TimelineDamageDealt(damageSource.globalObjectId,
            damageTarget.globalObjectId, amount));

    }
}