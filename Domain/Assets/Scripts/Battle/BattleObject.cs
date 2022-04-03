using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleObject
{
    public BattleExecutor executor;
    public int side;

    //what type of object
    public int objectId;
    //execution identifier
    public int globalObjectId;
    public string objectName;

    public int localTick;

    public BattleObject(BattleExecutor exec, int side, int id, string name)
    {
        executor = exec;
        this.side = side;
        objectId = id;
        globalObjectId = executor.SetGlobalObjectId();
        objectName = name;
        localTick = executor.globalTick;

        executor.eventHandler.TickUp += this.OnTickUp;
    }

    public virtual void OnTickUp()
    {
        
    }
}
