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

    public EnabledEvents eventSubscriptions { get; set; }

    public BattleObject(BattleExecutor exec, int side, string name)
    {
        Executor = exec;
        Side = side;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        Executor.GetAlliedObjects(this).Add(this);
    }

    public virtual void OnStartTurn() { }
    public virtual void OnEndTurn() { }

    public virtual void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, bool isSkill, bool isCrit) { }
    public virtual void OnUnitDeath(IBattleUnit deadUnit) { }
    public virtual void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount) { }
    public virtual void OnSpawn(IBattleObject source) { }
}
