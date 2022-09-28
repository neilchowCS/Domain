using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior
{
    public ObjectBehavior()
    {

    }

    public virtual void OnSpawn()
    {

    }

    public virtual void OnTickUp()
    {

    }

    public virtual void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount, DamageType damageType)
    {
        
    }

    public virtual void OnDamageTaken(IBattleUnit damageTarget, IBattleUnit damageSource, int amount)
    {
        
    }

    public virtual void OnUnitDeath(IBattleUnit deadUnit)
    {
        
    }

    public virtual void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount)
    {
        
    }
}
