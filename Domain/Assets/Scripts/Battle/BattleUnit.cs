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

    public enum MoveStates {noTarget, movingToTile, tileArrived, inRange};
    public MoveStates moveState;

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

        moveState = MoveStates.noTarget;

        executor.eventHandler.DamageDealt += this.OnDamageDealt;
        executor.eventHandler.DamageTaken += this.OnDamageTaken;
        executor.eventHandler.UnitDeath += this.OnUnitDeath;

        executor.timeline.AddTimelineEvent(new EventSpawn(id));
        Debug.Log("Spawned " + objectName + " (" + globalObjectId + ")");
    }

    public override void OnTickUp()
    {
        if (executor.IsRunning())
        {
            TickUpMove();
            TickUpAttack();
        }
    }

    public virtual void TickUpMove()
    {
        if (moveState == MoveStates.noTarget)
        {
            LookForward();
            if (BUnitHelperFunc.GetBattleUnitDistance(this, currentTarget) <= unitRange)
            {
                moveState = MoveStates.inRange;
            }
            else
            {
                moveState = MoveStates.movingToTile;
                targetTile = BUnitHelperFunc.GetNextBattleTile(this, currentTarget);
                if (targetTile.occupied)
                {
                    Debug.Log("Uh oh!");
                }
                targetTile.occupied = true;
            }
        }else if (moveState == MoveStates.movingToTile)
        {
            /*
            Debug.Log(objectName + " (" + globalObjectId + ") attempting to move");
            position = Vector3.MoveTowards(position, currentTarget.position, unitMoveSpeed);
            if (BUnitHelperFunc.GetBattleUnitDistance(this, currentTarget) <= unitRange)
            {
                moveState = MoveStates.inRange;
                Debug.Log(objectName + " (" + globalObjectId + ") moved in range");
            }
            */
            position = Vector3.MoveTowards(position, targetTile.position, unitMoveSpeed);
            if (Vector3.Distance(position, currentTile.position)
                < Vector3.Distance(position, targetTile.position))
            {
                currentTile.occupied = false;
                currentTile = targetTile;
            }
            if(Vector3.Distance(position, targetTile.position) < 0.000001f)
            {
                Debug.Log("Tile arrived");
                targetTile = null;
                if (BUnitHelperFunc.GetBattleUnitDistance(this, currentTarget) <= unitRange)
                {
                    moveState = MoveStates.inRange;
                    Debug.Log(objectName + " (" + globalObjectId + ") moved in range");
                }
                else
                {
                    moveState = MoveStates.tileArrived;
                }
                
            }
        }else if (moveState == MoveStates.tileArrived)
        {
            moveState = MoveStates.movingToTile;
            targetTile = BUnitHelperFunc.GetNextBattleTile(this, currentTarget);
            if (targetTile.occupied)
            {
                Debug.Log("Uh oh!");
            }
            targetTile.occupied = true;
        }
    }

    public virtual void TickUpAttack()
    {
        if (moveState == MoveStates.inRange)
        {
            DealDamage(currentTarget);
        }
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
            executor.eventHandler.TickUp -= this.OnTickUp;
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
            currentTarget = null;
            moveState = MoveStates.noTarget;
            Debug.Log(objectName + " (" + globalObjectId + ") has no target");
        }
    }

    public virtual void OnDamageTaken(BattleUnit damageTarget, BattleUnit damageSource, int amount)
    {

    }

    public virtual void LookForward()
    {
        currentTarget = BUnitHelperFunc.GetClosestEnemy(this) ?? BUnitHelperFunc.GetClosestEnemy(this);
        Debug.Log(objectName + " (" + globalObjectId + ") targeting "
            + currentTarget.objectName +" (" + currentTarget.globalObjectId + ")");
    }

}
