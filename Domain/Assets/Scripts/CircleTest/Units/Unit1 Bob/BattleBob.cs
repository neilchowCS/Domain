using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;
using System.Linq;

public class BattleBob : BattleUnit
{
    public BattleBob(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileX, int tileY) : base(exec, side, unitData, tileX, tileY)
    {

    }

    public override void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill)
    {
        if (damageTarget == this && damageSource is IBattleUnit &&
            (abilityType == AbilityType.Basic || abilityType == AbilityType.Skill))
        {
            /*
            Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(
                this, new() { (IBattleUnit)damageSource }, (int)(UnitData.unitMaxHealth.Value * 0.025f),
                DamageType.special, AbilityType.Passive).Cast<IEventTrigger>().ToList());
            */

            Executor.eventManager.InitiateTriggers(ActionExtension.ActionExtension.ProcessDamage(
                this, new() { (IBattleUnit)damageSource }, (int)(UnitData.unitMaxHealth.Value * 0.025f),
                DamageType.special, AbilityType.Passive));
        }
    }
}
