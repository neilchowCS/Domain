using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleObject
{
    public BattleExecutor Executor { get; set; }
    public int Side { get; set; }

    public int GlobalObjectId { get; set; }
    public string ObjectName { get; set; }

    public EnabledEvents EventSubscriptions { get; set; }

    public AttributeInt ObjSpeed { get; set; }

    public void OnStartTurn();
    public void OnEndTurn();

    public void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill);
    public void OnUnitDeath(IBattleUnit deadUnit);
    public void OnHealApplied(IBattleObject healSource, IBattleUnit healTarget, int amount);
    public void OnSpawn(IBattleObject source);
}