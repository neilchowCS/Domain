using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManagement
{
    public BattleExecutor executor;
    public bool isExecuting;

    /*
    public List<IBattleObject> orderedList;
    public Queue<List<IEventCommand>> commandQueue;

    private List<IBattleObject> additionOnHold;
    private List<IBattleObject> removalOnHold;
    private List<Hexagon> clearedTiles;
    */

    public Stack<List<IBattleObject>> eventStack;
    public Stack<IEventTrigger> eventTriggerStack;
    private List<IBattleUnit> deadUnits;

    public EventManagement(BattleExecutor exec)
    {
        executor = exec;
        isExecuting = false;
        /*
        orderedList = new();
        additionOnHold = new();
        removalOnHold = new();
        clearedTiles = new();
        commandQueue = new();
        */
        eventStack = new();
        eventTriggerStack = new();
        deadUnits = new();
    }

    public void ExecuteQueue()
    {
        isExecuting = true;

        while (eventStack.Count > 0)
        {
            if (eventStack.Peek().Count == 0)
            {
                eventStack.Pop();
                eventTriggerStack.Pop();
            }
            else
            {
                //iterated through event stack, execute all triggers on object
                //DO NOT REMOVE FROM END OF LIST
                Debug.Log(eventTriggerStack.Peek().Id);
                eventTriggerStack.Peek().Execute(eventStack.Peek()[0]);
                eventStack.Peek().RemoveAt(0);
                
            }
        }

        isExecuting = false;

        //CLEAR ALL DEATHS, EVENTS, ETC.

        ManageUnitDeath();
    }

    private void ManageUnitDeath()
    {
        foreach (IBattleUnit checkUnit in executor.activeUnits)
        {
            if (checkUnit.UnitData.health <= 0)
            {
                Debug.Log(checkUnit.ObjectName + " dead");
                //dead
                deadUnits.Add(checkUnit);
            }
        }

        ClearDeadReferences(deadUnits);
    }

    private void ClearDeadReferences(List<IBattleUnit> deadUnits)
    {
        foreach (IBattleUnit dead in deadUnits)
        {
            foreach (IBattleUnit unit in executor.activeUnits)
            {
                Debug.Log(unit == dead);
                unit.HandleDeath(dead);
            }
        }

        //set death flags
        bool set = false;
        while (deadUnits.Count > 0)
        {
            ManualInvokeTrigger(new UnitDeathTrigger(deadUnits[^1]));
            executor.activeUnits.Remove(deadUnits[^1]);
            if (deadUnits[^1].Side == 0)
            {
                Debug.Log("remove success:" + executor.player0Active.Remove(deadUnits[^1]));
                executor.player0Dead.Add(deadUnits[^1]);
            }
            else
            {
                Debug.Log("remove success:" + executor.player1Active.Remove(deadUnits[^1]));
                executor.player1Dead.Add(deadUnits[^1]);
            }
            deadUnits.RemoveAt(deadUnits.Count - 1);
            set = true;
        }
        if (set)
        {
            ExecuteQueue();
        }

        //TODO
    }

    public void InitiateTriggers(List<IEventTrigger> triggerList)
    {
        for (int i = triggerList.Count - 1; i >= 0; i--)
        {
            PushObjectQueue();
            eventTriggerStack.Push(triggerList[i]);
        }

        if (!isExecuting)
        {
            ExecuteQueue();
        }
    }

    public void ManualInvokeTrigger(IEventTrigger trigger)
    {
        PushObjectQueue();
        eventTriggerStack.Push(trigger);
    }

    public void AutoInvokeTrigger(IEventTrigger trigger)
    {
        PushObjectQueue();
        eventTriggerStack.Push(trigger);

        if (!isExecuting)
        {
            ExecuteQueue();
        }
    }

    public void EndRoundCleanup()
    {

    }

    //SORT OBJECTS BASED ON TRIGGER PRIORITY, THEN BY DEFAULT
    //Should all objects have x? no. THere should be set arrays of priority configurations so that they can be referenced
    private void PushObjectQueue()
    {
        //TODO remove need to initiate new list
        List<IBattleObject> temp = new();

        foreach (IBattleObject obj in executor.playerObjects0)
        {
            temp.Add(obj);
        }
        foreach (IBattleObject obj in executor.playerObjects1)
        {
            temp.Add(obj);
        }

        eventStack.Push(temp);
    }
}