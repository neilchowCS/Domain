using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ActionExtension
{
    public static class ActionExtension
    {
        public static List<DamageDealtCommand> ProcessDamage(IBattleUnit damageSource,
            List<IBattleUnit> damageTargets, int premitigationAmount, DamageType damageType, bool isSkill)
        {
            List<DamageDealtCommand> output = new();
            int postmitigationDamage = 0;
            bool crit = damageSource.Executor.rng.NextDouble() <= damageSource.UnitData.unitCritChance.Value;

            foreach (IBattleUnit damageTarget in damageTargets)
            {
                if (damageType == DamageType.normal || damageType == DamageType.special)
                {
                    if (damageTarget.UnitData.armorReduction < 1)
                    {
                        postmitigationDamage = (int)(premitigationAmount * damageTarget.UnitData.armorReduction);
                    }
                }
                if (crit)
                {
                    postmitigationDamage = (int)(postmitigationDamage * damageSource.UnitData.unitCrit.Value);
                }

                //damage reduction calcs here
                damageSource.Executor.logger.DealDamage(damageSource, postmitigationDamage, damageTarget, damageTarget.UnitData.health, damageTarget.UnitData.health - postmitigationDamage, crit);

                damageTarget.ModifyHealth(-postmitigationDamage, damageType, damageSource);
                damageTarget.Executor.CreateDamageNumber(damageTarget.Position, postmitigationDamage, damageType, crit);

                damageSource.Executor.UpdateProfileDamage(damageSource.GlobalObjectId, postmitigationDamage);
                output.Add(new DamageDealtCommand(damageSource, damageTarget, postmitigationDamage, damageType, isSkill, crit));
            }
            //damageSource.Executor.EnqueueEvent(new DamageDealtCommand(damageSource, damageTarget, postmitigationDamage, damageType));
            return output;
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