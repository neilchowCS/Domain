using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : BattleObject
{
    public int unitMaxHealth;
    public int unitHealth;

    public int unitAttack;

    //public int baseDefense;
    //public int baseMDefense;

    public float unitAttackSpeed;
    public float unitRange;
    public float unitMoveSpeed;
    /*
    public int baseMana;
    public int baseTickPerMana;
    public float baseCrit;
    public float baseCritChance;
    */

    public Vector3 position;

    public BattleTile currentTile;
    public BattleTile targetTile;
    public BattleUnit currentTarget;

    public BattleUnit(BattleExecutor exec, int side, int id, string name, int health, int attack,
        float attackSpeed, float range, float moveSpeed)
        : base(exec, side, id,name)
    {
        unitMaxHealth = health;
        unitHealth = unitMaxHealth;
        unitAttack = attack;
        unitAttackSpeed = attackSpeed;
        unitRange = range;
        unitMoveSpeed = moveSpeed;

        currentTile = BUnitHelperFunc.GetSpawnLoc(this);
        position = currentTile.position;
        currentTile.occupied = true;
        currentTarget = null;

        executor.eventHandler.DamageDealt += this.OnDamageDealt;
        executor.eventHandler.DamageTaken += this.OnDamageTaken;
        executor.eventHandler.UnitDeath += this.OnUnitDeath;

        executor.timeline.AddTimelineEvent(new EventSpawn(id));
        Debug.Log("Spawned " + objectName + " (" + globalObjectId + ")");
    }

    public virtual void DealDamage(BattleUnit damageTarget)
    {
        executor.eventHandler.OnDamageDealt(this, damageTarget, unitAttack);
    }

    public virtual void TakeDamage(BattleUnit damageSource, int amount)
    {
        unitHealth -= amount;
        executor.eventHandler.OnDamageTaken(this, damageSource, amount);
        if (unitHealth <= 0)
        {
            executor.eventHandler.OnUnitDeath(this);
        }
    }

    public virtual void OnDamageDealt(BattleUnit damageSource, BattleUnit damageTarget, int amount)
    {
        if (damageTarget == this)
        {
            TakeDamage(damageSource, amount);
        }
    }

    public virtual void OnUnitDeath(BattleUnit deadUnit)
    {
        if (deadUnit == this)
        {
            if (side == 0)
            {
                executor.player0.Remove(this);
                executor.player0Dead.Add(this);
            }
            else
            {
                executor.player1.Remove(this);
                executor.player1Dead.Add(this);
            }
        }

        if (deadUnit == currentTarget)
        {
            LookForward();
        }
    }

    public virtual void OnDamageTaken(BattleUnit damageTarget, BattleUnit damageSource, int amount)
    {

    }

    public virtual void LookForward()
    {
        currentTarget = BUnitHelperFunc.GetClosestEnemy(this) ?? BUnitHelperFunc.GetClosestEnemy(this);
    }

}
