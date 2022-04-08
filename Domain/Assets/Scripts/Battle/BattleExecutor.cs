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
        }
        else
        {
            Debug.Log(player0.Count != 0 ? "Player won!" : "Player lost!");
            run = false;
            timeline.AddTimelineEvent(new TimelineEnd());
            replayExecutor.StartReplay(timeline);
        }
        if (globalTick> 200)
        {
            Debug.Log("Player timed out!");
            run = false;
        }
    }

    public bool IsRunning()
    {
        return player0.Count > 0 && player1.Count > 0;
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

        playerUnits = new List<BattleUnit>[] { player0, player0Dead, player1 , player1Dead};
        playerObjects = new List<BattleObject>[] { playerObjects0, playerObjects1 };

        //init team
        TeamData team0 = new TeamData(dataListSO, 0);
        TeamData team1 = new TeamData(dataListSO, 1);

        foreach (UnitData i in team0.unitList)
        {
            player0.Add(i.GetBattleUnit(this, 0));
        }
        foreach (UnitData i in team1.unitList)
        {
            player1.Add(i.GetBattleUnit(this, 1));
        }
    }

}