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
                eventTriggerStack.Peek().Execute(eventStack.Peek()[0]);
                eventStack.Peek().RemoveAt(0);
            }
        }

        //CLEAR ALL DEATHS, EVENTS, ETC.

        isExecuting = false;
    }

    public void ManualInvokeTrigger(IEventTrigger trigger)
    {
        PushObjectQueue();
        eventTriggerStack.Push(trigger);
        /*
        if (!isExecuting)
        {
            ExecuteQueue();
        }
        */
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

    private void PushObjectQueue()
    {
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