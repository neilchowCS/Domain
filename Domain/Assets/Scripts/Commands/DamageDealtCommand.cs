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
    private bool isSkill;
    private bool isCrit;

    public DamageDealtCommand(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType, bool isSkill, bool isCrit)
    {
        this.damageSource = damageSource;
        this.damageTarget = damageTarget;
        this.amount = amount;
        this.damageType = damageType;
        this.isSkill = isSkill;
        this.isCrit = isCrit;
    }

    /// <summary>
    /// Raises DealDamage event.
    /// </summary>
    public void Execute(IBattleObject obj)
    {
        obj.OnDamageDealt(damageSource, damageTarget, amount, damageType, isSkill, isCrit);
    }
}