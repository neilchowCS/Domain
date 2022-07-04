using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base object during battle.
/// </summary>
public class BattleObject
{
    public BattleExecutor executor;
    public int side;

    /// <summary>
    /// Global identifier.
    /// </summary>
    public int globalObjectId;

    public string objectName;

    public bool isInstantiated = false;

    public BattleObject(BattleExecutor exec, int side)
    {
        executor = exec;
        this.side = side;
        globalObjectId = executor.SetGlobalObjectId();

        //localTick = executor.globalTick;
        executor.eventHandler.TickUp += this.OnTickUp;
    }

    public BattleObject(BattleExecutor exec, int side, string name)
    {
        executor = exec;
        this.side = side;

        globalObjectId = executor.SetGlobalObjectId();
        objectName = name;
        //localTick = executor.globalTick;
        
        executor.eventHandler.TickUp += this.OnTickUp;
    }


    public virtual void OnTickUp()
    {
        
    }
}
