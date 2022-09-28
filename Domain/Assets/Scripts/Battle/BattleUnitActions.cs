using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unobserved specific, superclass
/// </summary>
public class BattleUnitActions
{
    public IBattleUnit iUnit;

    public BattleUnitActions(IBattleUnit unit)
    {
        iUnit = unit;
    }

    /// <summary>
    /// Decreases this unit's health.
    /// Raises TakeDamage event.
    /// Checks if this is dead.
    /// If true, raises UnitDeath event.
    /// </summary>
    public virtual void TakeDamage(IBattleUnit damageSource, int amount)
    {
        iUnit.UnitData.health -= amount;
        iUnit.Executor.eventHandler.OnDamageTaken(iUnit, damageSource, amount);
        if (iUnit.UnitData.health <= 0)
        {
            iUnit.Executor.eventHandler.OnUnitDeath(iUnit);
        }
    }

    public virtual void ReceiveHeal(IBattleUnit healSource, int amount)
    {
        iUnit.UnitData.health += amount;

    }

    public virtual void DealtDamage(int amount)
    {

    }

    public virtual void SetMana(int amount)
    {
        iUnit.UnitData.mana = amount;
    }

    public virtual void SelfDeath()
    {
        
    }

    public virtual void NewProjectile(IBattleUnit source,
        int index, IBattleUnit target)
    {
        
    }

    public virtual void NewProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        
    }
}