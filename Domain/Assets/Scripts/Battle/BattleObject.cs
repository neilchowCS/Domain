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
    public int GlobalObjectId { get; set; }

    [field: SerializeField]
    public string ObjectName { get; set; }

    public virtual AttributeInt ObjSpeed { get; set; }

    public EnabledEvents EventSubscriptions { get; set; }

    public BattleObject(BattleExecutor exec, int side, string name, int speed)
    {
        Executor = exec;
        Side = side;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        ObjSpeed = new(speed);

        exec.eventManager.AddObject(this);
    }

    public BattleObject(BattleExecutor exec, int side, string name, IBattleObject dependent)
    {
        Executor = exec;
        Side = side;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        ObjSpeed = dependent.ObjSpeed;

        exec.eventManager.AddObject(this);
    }

    public virtual void OnStartTurn() { }
    public virtual void OnEndTurn() { }

    public virtual void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill) { }
    public virtual void OnUnitDeath(IBattleUnit deadUnit) { }
    public virtual void OnHealApplied(IBattleObject healSource, IBattleUnit healTarget, int amount) { }
    public virtual void OnSpawn(IBattleObject source) { }
}
