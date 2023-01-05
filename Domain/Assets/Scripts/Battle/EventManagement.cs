using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManagement
{
    public BattleExecutor executor;

    public Queue<List<IEventCommand>> commandQueue;

    public List<IBattleUnit> deadUnits;
    public List<IBattleStatus> clearedStatus;

    public EventManagement(BattleExecutor exec)
    {
        executor = exec;
        commandQueue = new();
        deadUnits = new();
        clearedStatus = new();
    }

    public void Initialize(List<IBattleUnit> units)
    {

    }

    public void RemoveUnit(IBattleUnit unit)
    {
        Debug.Log("Removing unit");
        bool found = false;
        /*
        for (int i = 0; i < orderedObjects.Count; i++)
        {
            if (orderedObjects[i][^1][0] == unit)
            {
                orderedObjects[i][2].Remove(unit);
                orderedObjects[i][3].Remove(unit);
                found = true;
                break;
            }
        }
        */
        if (!found)
        {
            Debug.Log("ERROR!!! UNIT NOT FOUND!!!");
        }
    }

    public void ExecuteQueue()
    {
        System.Console.WriteLine("execute queue");
        while (commandQueue.Count > 0)
        {
            List<IEventCommand> commandList = commandQueue.Peek();
            foreach (IEventCommand command in commandList)
            {
                foreach (IBattleObject obj in executor.sortedStack)
                {
                    command.Execute(obj);
                }
            }
            commandQueue.Dequeue();

            Debug.Log((commandQueue.Count) + " " + ((commandQueue.Count > 0) ? "repeat" : "exit"));
        }
        Debug.Log("clearing");
        CleanUp();
        //somehow causing bug???
        ClearStatus();
        ClearUnits();
        if (deadUnits.Count >= 0)
        {
            Debug.Log("unit death event");
            InvokeUnitDeath(deadUnits);
        }
        Debug.Log("queue completed");

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
                deadUnits.Add(deadUnit);
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
            }
        }
    }

    public void ClearUnits()
    {
        Debug.Log(deadUnits.Count);
        for (int i = 0; i < deadUnits.Count; i++)
        {
            RemoveUnit(deadUnits[0]);
            deadUnits.RemoveAt(0);
        }
    }

    public void ClearStatus()
    {
        Debug.Log(clearedStatus.Count);
        for (int i = 0; i < clearedStatus.Count; i++)
        {
            clearedStatus[0].RemoveStatus();
            clearedStatus.RemoveAt(0);
        }
    }

    public void InvokeStartTurn()
    {
        foreach (IBattleObject obj in executor.sortedStack)
        {
            obj.OnStartTurn();
        }
    }

    public void InvokeDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType, AbilityType abilityType, bool isCrit)
    {
        foreach (IBattleObject obj in executor.sortedStack)
        {
            obj.OnDamageDealt(damageSource, damageTarget, amount, damageType, abilityType, isCrit);
        }
    }

    public void InvokeUnitDeath(List<IBattleUnit> units)
    {
        foreach (IBattleObject obj in executor.sortedStack)
        {
            foreach (IBattleUnit unit in units)
            {
                obj.OnUnitDeath(unit);
            }
        }
    }
}