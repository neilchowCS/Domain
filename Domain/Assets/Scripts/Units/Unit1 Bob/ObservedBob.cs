using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedBob : ObservedUnit
{
    public BobAnimController animController;

    public override void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit)
    {
        if (damageTarget == this && damageSource is IBattleUnit &&
            (abilityType == AbilityType.Basic || abilityType == AbilityType.Skill))
        {
            animController.CreateThorns();
            Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(
                this, new() { (IBattleUnit)damageSource }, (int)(UnitData.unitMaxHealth.Value * 0.025f),
                DamageType.special, AbilityType.Passive).Cast<IEventCommand>().ToList());
        }
    }
}
