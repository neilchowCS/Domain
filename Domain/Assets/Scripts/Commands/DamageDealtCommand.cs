using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealtCommand : IEventCommand
{
    public int Id { get; set; } = 2;
    private IBattleUnit damageSource;
    private IBattleUnit damageTarget;
    private int amount;
    private DamageType damageType;

    public DamageDealtCommand(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType)
    {
        this.damageSource = damageSource;
        this.damageTarget = damageTarget;
        this.amount = amount;
        this.damageType = damageType;
    }

    /// <summary>
    /// Raises DealDamage event.
    /// </summary>
    public void Execute(IBattleObject obj)
    {
        obj.OnDamageDealt(damageSource, damageTarget, amount, damageType);
    }
}