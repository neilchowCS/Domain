using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedBob : ObservedUnit
{
    public override void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit)
    {
        if (damageTarget == this && (abilityType == AbilityType.Basic || abilityType == AbilityType.Skill))
        {

            Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(
                this, new() { damageSource }, (int)(UnitData.unitMaxHealth.Value * 0.05f),
                DamageType.special, AbilityType.Passive).Cast<IEventCommand>().ToList());
        }
    }
}
