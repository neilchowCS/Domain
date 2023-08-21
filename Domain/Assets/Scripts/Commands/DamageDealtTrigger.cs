using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealtTrigger : IEventTrigger
{
    public int Id { get; set; }

    private IBattleObject damageSource;
    private IBattleUnit damageTarget;
    private int amount;
    private DamageType damageType;
    private AbilityType abilityType;
    private bool isCrit;
    private int overkill;

    public DamageDealtTrigger(IBattleObject damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill)
    {
        Id = 2;
        this.damageSource = damageSource;
        this.damageTarget = damageTarget;
        this.amount = amount;
        this.damageType = damageType;
        this.abilityType = abilityType;
        this.isCrit = isCrit;
        this.overkill = overkill;
    }

    /// <summary>
    /// Raises DealDamage event.
    /// </summary>
    public void Execute(IBattleObject obj)
    {
        obj.OnDamageDealt(damageSource, damageTarget, amount, damageType, abilityType, isCrit, overkill);
    }
}