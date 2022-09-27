using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCommand : ISubcommand
{
    private IBattleUnit damageSource;
    private IBattleUnit damageTarget;
    private int amount;
    private DamageType damageType;

    public DamageCommand(IBattleUnit damageSource, IBattleUnit damageTarget,
        int amount, DamageType damageType)
    {
        this.damageSource = damageSource;
        this.damageTarget = damageTarget;
        this.amount = amount;
        this.damageType = damageType;
    }

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public void Execute()
    {
        if (damageType == DamageType.normal)
        {
            if (damageTarget.UnitData.armorReduction < 1)
            {
                amount = (int)(amount * damageTarget.UnitData.armorReduction);
            }
        }

        //damage reduction calcs here
        damageSource.Executor.eventHandler.OnDamageDealt(damageSource, damageTarget, amount);
    }
}
