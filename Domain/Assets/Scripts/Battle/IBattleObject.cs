using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleObject
{
    public BattleExecutor Executor { get; set; }
    public int Side { get; set; }

    public int GlobalObjectId { get; set; }
    public string ObjectName { get; set; }

    public EnabledEvents eventSubscriptions { get; set; }

    public void OnStartTurn();
    public void OnEndTurn();

    public void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, bool isSkill, bool isCrit);
    public void OnUnitDeath(IBattleUnit deadUnit);
    public void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount);
    public void OnSpawn(IBattleObject source);
}