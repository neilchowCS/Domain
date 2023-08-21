using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedBob : ObservedUnit
{
    public BobAnimController animController;

    public override void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill)
    {
        base.OnDamageDealt(damageSource, damageTarget, amount, damageType, abilityType, isCrit, overkill);
        if (damageTarget == this && damageSource is IBattleUnit &&
            (abilityType == AbilityType.Basic || abilityType == AbilityType.Skill))
        {
            animController.CreateThorns();
            Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(
                this, new() { (IBattleUnit)damageSource }, (int)(UnitData.unitMaxHealth.Value * 0.025f),
                DamageType.special, AbilityType.Passive).Cast<IEventTrigger>().ToList());
        }
    }
}
