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
    private List<Hexagon> clearedTiles;

    public EventManagement(BattleExecutor exec)
    {
        executor = exec;
        orderedList = new();
        additionOnHold = new();
        removalOnHold = new();
        clearedTiles = new();
        commandQueue = new();
    }

    public void ExecuteQueue()
    {
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

            HandleDeadUnits(FindDeadUnits());

            commandQueue.Dequeue();

            foreach (IBattleObject obj in removalOnHold)
            {
                DestructiveRemoveObject(obj);
            }
            removalOnHold = new();

            if (additionOnHold.Count > 0)
            {
                foreach (IBattleObject obj in additionOnHold)
                {
                    DestructiveAddObject(obj);
                }
                additionOnHold = new();
                OrderSpeedList();
            }

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
        orderedList.Add(obj);
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

        if (obj is IBattleStatus o)
        {
            o.RemoveStatus();
        }
    }

    public List<IBattleUnit> FindDeadUnits()
    {
        List<IBattleUnit> output = new();
        for (int i = 0; i < executor.activeUnits.Count; i++)
        {
            if (executor.activeUnits[i].UnitData.health <= 0)
            {
                output.Add(executor.activeUnits[i]);
            }
        }
        return output;
    }

    public void HandleDeadUnits(List<IBattleUnit> deadUnits)
    {
        foreach (IBattleUnit deadUnit in deadUnits)
        {
            clearedTiles.Add(executor.hexMap[deadUnit.X, deadUnit.Y]);
            ClearTargetReferences(deadUnit);
            executor.logger.UnitDeath(deadUnit);
            RemoveObject(deadUnit);
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

        if (deadUnits.Count >= 0)
        {
            OrderSpeedList();
            InvokeUnitDeath(deadUnits);
        }
    }

    public void ClearTargetReferences(IBattleUnit deadUnit)
    {
        //NOTE: REVIVED UNITS MUST HAVE REFERENCES CLEARED
        foreach (IBattleUnit u in executor.activeUnits)
        {
            if (u.CurrentTarget == deadUnit)
            {
                u.CurrentTarget = null;
            }
        }
    }

    public void ClearEmptyTiles()
    {
        foreach (Hexagon h in clearedTiles)
        {
            h.occupant = null;
        }
        clearedTiles = new();
    }

    public void InvokeStartTurn()
    {
        foreach (IBattleObject obj in orderedList)
        {
            obj.OnStartTurn();
        }
    }

    public void InvokeEndTurn()
    {
        foreach (IBattleObject obj in orderedList)
        {
            obj.OnEndTurn();
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

    public void UnitDeathResponse(IBattleUnit unit)
    {

    }
}