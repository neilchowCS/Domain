using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManagement
{
    public BattleExecutor executor;
    public List<List<IBattleObject>[]> orderedObjects;
    //Layer 1: list of array of list of objects : each speed
    //Layer 2: array of obj list : each event
    //Layer 3: obj list : each subscriber

    public Queue<List<IEventCommand>> commandQueue;

    public EventManagement(BattleExecutor exec)
    {
        executor = exec;
        orderedObjects = new();
        commandQueue = new();
    }

    public void Initialize(List<IBattleUnit> units)
    {
        //FIXME
        int count = 6;//units[0].UnitData.baseData.eventSubscriptions.events.Count;
        //Debug.Log(orderedObjects[^1].Length + " events");
        for (int i = 0; i < units.Count; i++)
        {
            orderedObjects.Add(new List<IBattleObject>[count + 1]);
            for (int j = 0; j < orderedObjects[i].Length; j++)
            {
                orderedObjects[i][j] = new List<IBattleObject>();
            }
            //ordered object[i] = ith speed tier, get event array
            //ordered object[^1] = last event array, get list
            orderedObjects[i][3].Add(units[i]);
            orderedObjects[i][^1].Add(units[i]);
        }

        //for (int i = 0; i < orderedObjects.Count; i++)
        {
            //Debug.Log(orderedObjects[i][^1][0].ObjectName);
        }
    }

    public void ExecuteQueue()
    {
        while (commandQueue.Count > 0)
        {
            //first, go through each speed tier, go through each command in list, then go next command list
            List<IEventCommand> commandList = commandQueue.Dequeue();
            foreach (List<IBattleObject>[] speedTier in orderedObjects)
            {
                foreach (IEventCommand command in commandList)
                {
                    foreach (IBattleObject obj in speedTier[command.Id])
                    {
                        command.Execute(obj);
                    }
                }
            }
        }
        CleanUp();
        
    }

    public void CleanUp()
    {
        //check dead, if dead, execute queue

        for (int i = 0; i < executor.activeUnits.Count; i++)
        {
            if (executor.activeUnits[i].UnitData.health <= 0)
            {
                //FIXME
                IBattleUnit deadUnit = executor.activeUnits[i];
                executor.logger.UnitDeath(deadUnit);

                i--;
                executor.activeUnits.Remove(deadUnit);
                if (deadUnit.Side == 0)
                {
                    deadUnit.Executor.player0Active.Remove(deadUnit);
                    deadUnit.Executor.player0Dead.Add(deadUnit);
                }
                else
                {
                    deadUnit.Executor.player1Active.Remove(deadUnit);
                    deadUnit.Executor.player1Dead.Add(deadUnit);
                }
                InvokeUnitDeath(deadUnit);
                ExecuteQueue();
            }
        }
    }

    public void InvokeStartTurn()
    {
        foreach (List<IBattleObject>[] speedTier in orderedObjects)
        {
            foreach (IBattleObject obj in speedTier[0])
            {
                obj.OnStartTurn();
            }
        }
    }

    public void InvokeEndTurn()
    {
        //Debug.Log(orderedObjects[0].Length);
        foreach (List<IBattleObject>[] speedTier in orderedObjects)
        {
            foreach (IBattleObject obj in speedTier[1])
            {
                obj.OnEndTurn();
            }
        }
    }

    public void InvokeDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType, bool isSkill, bool isCrit)
    {
        foreach (List<IBattleObject>[] speedTier in orderedObjects)
        {
            foreach (IBattleObject obj in speedTier[2])
            {
                obj.OnDamageDealt(damageSource, damageTarget, amount, damageType, isSkill, isCrit);
            }
        }
    }

    public void InvokeUnitDeath(IBattleUnit unit)
    {
        foreach (List<IBattleObject>[] speedTier in orderedObjects)
        {
            foreach (IBattleObject obj in speedTier[3])
            {
                obj.OnUnitDeath(unit);
            }
        }
    }
}