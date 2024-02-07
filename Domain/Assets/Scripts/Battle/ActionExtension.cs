using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ActionExtension
{
    public static class ActionExtension
    {
        public static List<IEventTrigger> ProcessDamage(IBattleObject damageSource,
            List<IBattleUnit> damageTargets, int premitigationAmount, DamageType damageType, AbilityType abilityType)
        {
            List<IEventTrigger> output = new();

            foreach (IBattleUnit damageTarget in damageTargets)
            {
                int postmitigationDamage = premitigationAmount;

                if (damageType == DamageType.normal)
                {
                    if (damageTarget.UnitData.armorReduction < 1)
                    {
                        postmitigationDamage = (int)(premitigationAmount * damageTarget.UnitData.armorReduction);
                    }
                }

                bool crit = false;
                if (damageSource is IBattleUnit)
                {
                    crit = (abilityType == AbilityType.Basic || abilityType == AbilityType.Skill) &&
                        damageSource.Executor.rng.NextDouble() <= ((IBattleUnit)damageSource).UnitData.unitCritChance.Value;
                    if (crit)
                    {
                        postmitigationDamage = (int)(postmitigationDamage * ((IBattleUnit)damageSource).UnitData.unitCrit.Value);
                    }

                    damageSource.Executor.UpdateProfileDamage(damageSource.GlobalObjectId, postmitigationDamage);
                }

                //damage reduction calcs here
                damageSource.Executor.logger.DealDamage(damageSource, postmitigationDamage, damageTarget, damageTarget.UnitData.health, damageTarget.UnitData.health - postmitigationDamage, crit);

                damageTarget.ModifyHealth(-postmitigationDamage, damageType);
                damageTarget.Executor.CreateDamageNumber(damageTarget.Position, postmitigationDamage, damageType, crit);

                int overkill = (damageTarget.UnitData.health > 0) ? -1 : -damageTarget.UnitData.health;

                output.Add(new DamageDealtTrigger(damageSource, damageTarget, postmitigationDamage, damageType, abilityType, crit, overkill));
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

            healTarget.ModifyHealth(amount, DamageType.healing);

            //eventHandler.OnHealApplied(healSource, healTarget, amount);
        }

        public static List<IBattleStatus> NewBurnStatus(IBattleObject source, IBattleUnit host,
            int duration, int dmgPerTick)
        {
            List<IBattleStatus> l = new();
            IBattleStatus output = null;
            if (host is ObservedUnit)
            {
                output = source.Executor.factory.NewObservedStatusBurn(source, host, duration, dmgPerTick);
            }
            else
            {
                output = new StatusBurn(source.Executor, source.Side, source, host, duration, dmgPerTick);
            }
            AddStatus(source, host, output);
            //FIXME
            output.OnSpawn(output);

            return l;
        }

        public static void AddStatus(IBattleObject source, IBattleUnit host, IBattleStatus status)
        {
            host.StatusList.Add(status);
        }
    }
}