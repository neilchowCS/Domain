using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventManagement
{
    public BattleExecutor executor;

    public List<IBattleObject> orderedList;
    public Queue<List<IEventCommand>> commandQueue;

    private List<IBattleObject> additionOnHold;
    private List<IBattleObject> removalOnHold;

    public List<IBattleUnit> deadUnits;

    public EventManagement(BattleExecutor exec)
    {
        executor = exec;
        orderedList = new();
        additionOnHold = new();
        removalOnHold = new();
        commandQueue = new();
        deadUnits = new();
    }

    public void ExecuteQueue()
    {
        System.Console.WriteLine("execute queue");
        while (commandQueue.Count > 0)
        {
            List<IEventCommand> commandList = commandQueue.Peek();
            foreach (IBattleObject obj in orderedList)
            {
                foreach (IEventCommand command in commandList)
                {
                    command.Execute(obj);
                }
            }


            Debug.Log("clearing");
            CleanUp();
            //somehow causing bug???
            
            if (deadUnits.Count >= 0)
            {
                OrderSpeedList();
                Debug.Log("unit death event");
                InvokeUnitDeath(deadUnits);
                deadUnits = new();
            }

            foreach (IBattleUnit u in deadUnits)
            {
                DestructiveRemoveObject(u);
            }

            commandQueue.Dequeue();
            //DEAL WITH WITHHELD OBJECTS
            foreach (IBattleObject obj in removalOnHold)
            {
                DestructiveRemoveObject(obj);
            }
            removalOnHold = new();
            foreach (IBattleObject obj in additionOnHold)
            {
                DestructiveAddObject(obj);
            }
            additionOnHold = new();
            OrderSpeedList();

            Debug.Log((commandQueue.Count) + " " + ((commandQueue.Count > 0) ? "repeat" : "exit"));
        }
        Debug.Log("queue completed");

    }

    public void OrderSpeedList()
    {
        orderedList = orderedList.OrderByDescending(o => o.ObjSpeed.Value).ToList();
    }

    public void AddObject(IBattleObject obj)
    {
        if (executor.isInitializing)
        {
            DestructiveAddObject(obj);
        }
        else
        {
            additionOnHold.Add(obj);
        }
    }

    private void DestructiveAddObject(IBattleObject obj)
    {
        //ERROR SOURCE HERE!!!
        orderedList.Add(obj);
        //orderedList = orderedList.OrderBy(o => o.ObjSpeed).ToList();
        switch (obj.Side)
        {
            case 0:
                executor.playerObjects0.Add(obj);
                break;
            default:
                executor.playerObjects1.Add(obj);
                break;
        }
    }

    public void RemoveObject(IBattleObject obj)
    {
        if (executor.isInitializing)
        {
            DestructiveRemoveObject(obj);
        }
        else
        {
            removalOnHold.Add(obj);
        }
    }

    private void DestructiveRemoveObject(IBattleObject obj)
    {
        orderedList.Remove(obj);
        switch (obj.Side)
        {
            case 0:
                executor.playerObjects0.Remove(obj);
                break;
            default:
                executor.playerObjects1.Remove(obj);
                break;
        }

        if(obj is IBattleStatus o)
        {
            o.RemoveStatus();
        }
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

    public void InvokeStartTurn()
    {
        foreach (IBattleObject obj in orderedList)
        {
            obj.OnStartTurn();
        }
    }

    public void InvokeDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType, AbilityType abilityType, bool isCrit)
    {
        foreach (IBattleObject obj in orderedList)
        {
            obj.OnDamageDealt(damageSource, damageTarget, amount, damageType, abilityType, isCrit);
        }
    }

    public void InvokeUnitDeath(List<IBattleUnit> units)
    {
        foreach (IBattleObject obj in orderedList)
        {
            foreach (IBattleUnit unit in units)
            {
                obj.OnUnitDeath(unit);
            }
        }
    }
}