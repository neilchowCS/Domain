using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleExecutor : MonoBehaviour
{
    public bool run = true;
    public ReplayExecutor replayExecutor;

    public Timeline timeline;

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
        int teamSize0 = 1;
        int teamSize01 = 1;
        for (int i = 0; i < teamSize0; i++)
        {
            BattleUnit test = new AliceBU(this, 0);

            player0.Add(test);
        }
        for (int i = 0; i < teamSize01; i++)
        {
            BattleUnit test = new BobBU(this, 0);

            player0.Add(test);
        }

        int teamSize1 = 2;
        for (int i = 0; i < teamSize1; i++)
        {
            BattleUnit test = new AliceBU(this, 1);

            player1.Add(test);
        }
    }

}