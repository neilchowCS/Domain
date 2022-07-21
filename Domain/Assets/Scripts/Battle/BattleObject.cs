using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Base object during battle.
/// </summary>
public class BattleObject : IBattleObject
{
    [field: SerializeField]
    public BattleExecutor Executor { get; set; }

    [field: SerializeField]
    public int Side { get; set; }

    [field: SerializeField]
    public int GlobalObjectId { get; }

    [field: SerializeField]
    public string ObjectName { get; set; }

    public virtual ObjectBehavior Behavior { get; }

    public BattleObject(BattleExecutor exec, int side, string name)
    {
        Executor = exec;
        Side = side;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        Executor.eventHandler.TickUp += Behavior.OnTickUp;
    }
}
