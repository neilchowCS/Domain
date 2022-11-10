using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ActionExtension
{
    public static class ActionExtension
    {
        public static void DealDamage(IBattleUnit damageSource,
            List<IBattleUnit> damageTargets, int amount, DamageType damageType)
        {
            //FIXME premitigation should be float
            int postmitigationDamage = 0;
            foreach (IBattleUnit damageTarget in damageTargets)
            {
                if (damageType == DamageType.normal || damageType == DamageType.special)
                {
                    if (damageTarget.UnitData.armorReduction < 1)
                    {
                        postmitigationDamage = (int)(amount * damageTarget.UnitData.armorReduction);
                    }
                }

                //damage reduction calcs here
                damageSource.Executor.logger.DealDamage(damageSource, postmitigationDamage, damageTarget, damageTarget.UnitData.health, damageTarget.UnitData.health - postmitigationDamage);

                damageTarget.ModifyHealth(-postmitigationDamage, damageType, damageSource);
                damageTarget.Executor.CreateDamageNumber(damageTarget.Position, postmitigationDamage, damageType);

                damageSource.Executor.UpdateProfileDamage(damageSource.GlobalObjectId, postmitigationDamage);
                damageSource.Executor.EnqueueEvent(new DamageDealtCommand(damageSource, damageTarget, postmitigationDamage, damageType));
            }
        }

        public static void ApplyHeal(IBattleUnit healSource, IBattleUnit healTarget, int amount)
        {
            Debug.Log(amount);
            //apply healing reduction before
            if (healTarget.UnitData.health + amount > healTarget.UnitData.unitMaxHealth.Value)
            {
                amount = healTarget.UnitData.unitMaxHealth.Value - healTarget.UnitData.health;
            }

            if (healSource == null)
            {
                Debug.Log("healing error!");
            }

            //modulation

            healTarget.ModifyHealth(amount, DamageType.healing, healSource);

            //eventHandler.OnHealApplied(healSource, healTarget, amount);
        }
    }
}