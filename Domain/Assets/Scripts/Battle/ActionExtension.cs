using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BehaviorExtension
{
    public static class ActionExtension
    {
        public static void DealDamage(IBattleUnit damageSource,
            List<IBattleUnit> damageTargets, int amount, DamageType damageType)
        {
            foreach (IBattleUnit damageTarget in damageTargets)
            {
                if (damageType == DamageType.normal)
                {
                    if (damageTarget.UnitData.armorReduction < 1)
                    {
                        amount = (int)(amount * damageTarget.UnitData.armorReduction);
                    }
                }

                //damage reduction calcs here

                damageTarget.ModifyHealth(-amount, damageType, damageSource);
                damageTarget.Executor.CreateDamageNumber(damageTarget.Position, amount, damageType);
                damageSource.Executor.EnqueueEvent(new DamageDealtCommand(damageSource, damageTarget, amount, damageType));
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