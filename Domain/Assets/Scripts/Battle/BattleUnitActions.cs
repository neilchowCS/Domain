using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unobserved specific, superclass
/// </summary>
public class BattleUnitActions
{
    protected readonly IBattleUnit unit;

    public BattleUnitActions(IBattleUnit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// Decreases this unit's health.
    /// Raises TakeDamage event.
    /// Checks if this is dead.
    /// If true, raises UnitDeath event.
    /// </summary>
    public virtual void TakeDamage(IBattleUnit damageSource, int amount)
    {
        unit.UnitData.health -= amount;
        unit.Executor.eventHandler.OnDamageTaken(unit, damageSource, amount);
        if (unit.UnitData.health <= 0)
        {
            unit.Executor.eventHandler.OnUnitDeath(unit);
        }
    }

    public virtual void ReceiveHeal(IBattleUnit healSource, int amount)
    {
        unit.UnitData.health += amount;

    }

    public virtual void SetMana(int amount)
    {
        unit.UnitData.mana = amount;
    }

    public virtual void ModifyMana(int amount)
    {
        unit.UnitData.mana += amount;
    }

    public virtual void SelfDeath()
    {
        
    }

    public virtual void NewProjectile(IBattleUnit source,
        int index, IBattleUnit target)
    {
        unit.Executor.factory.NewProjectile(source, index, target);
    }

    public virtual void NewProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        unit.Executor.factory.NewProjectile(source, index, target);
    }
}