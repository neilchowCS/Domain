using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unit created during battle sim.
/// </summary>
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

    public enum MoveStates { noTarget, movingToTile, tileArrived, inRange, stopped };
    /// <summary>
    /// noTarget, movingToTile, tileArrived, inRange, stopped.
    /// </summary>
    public MoveStates moveState;

    /// <summary>
    /// FIX ME: event subscription
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, int id, string name, int health, int attack,
        float attackSpeed, float range, float moveSpeed)
        : base(exec, side, id, name)
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

        //FIX ME
        executor.eventHandler.DamageDealt += this.OnDamageDealt;
        executor.eventHandler.DamageTaken += this.OnDamageTaken;
        executor.eventHandler.UnitDeath += this.OnUnitDeath;

        executor.timeline.AddTimelineEvent(new TimelineSpawn(id, globalObjectId, side,
            0, position.x, position.y, position.z, unitMaxHealth));
        Debug.Log("Spawned " + objectName + " (" + globalObjectId + ")");
    }

    /// <summary>
    /// Event subscriber.
    /// Logic for every tick during battle while running.
    /// </summary>
    public override void OnTickUp()
    {
        if (executor.IsRunning())
        {
            TickUpMove();
            TickUpAttack();
        }
    }

    /// <summary>
    /// Handles movement during OnTickUp().
    /// </summary>
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
                //FIX ME
                moveState = MoveStates.movingToTile;
                targetTile = BUnitHelperFunc.GetNextBattleTile(this, currentTarget);
                executor.timeline.AddTimelineEvent(new TimelineMove(globalObjectId,
                0, targetTile.position.x, targetTile.position.y, targetTile.position.z));
                if (targetTile.occupied)
                {
                    Debug.Log("Uh oh!");
                }
                targetTile.occupied = true;
            }
        }
        else if (moveState == MoveStates.movingToTile)
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
            if (Vector3.Distance(position, targetTile.position) < 0.000001f)
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
        }
        else if (moveState == MoveStates.tileArrived)
        {
            moveState = MoveStates.movingToTile;
            targetTile = BUnitHelperFunc.GetNextBattleTile(this, currentTarget);
            executor.timeline.AddTimelineEvent(new TimelineMove(globalObjectId,
                0, targetTile.position.x, targetTile.position.y, targetTile.position.z));
            if (targetTile.occupied)
            {
                Debug.Log("Uh oh!");
            }
            targetTile.occupied = true;
        }
    }

    /// <summary>
    /// Handles attacking during OnTickUp().
    /// </summary>
    public virtual void TickUpAttack()
    {
        if (moveState == MoveStates.inRange)
        {
            DealDamage(currentTarget);
        }
    }

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public virtual void DealDamage(BattleUnit damageTarget)
    {
        executor.eventHandler.OnDamageDealt(this, damageTarget, unitAttack);
        executor.timeline.AddTimelineEvent(new TimelineDamageDealt(this.globalObjectId,
            damageTarget.globalObjectId, unitAttack));
    }

    //FIX ME: unsubscribe from events

    /// <summary>
    /// Decreases this unit's health.
    /// Raises TakeDamage event.
    /// Checks if this is dead.
    /// If true, unsubscribes and raises UnitDeath event.
    /// </summary>
    public virtual void TakeDamage(BattleUnit damageSource, int amount)
    {
        unitHealth -= amount;
        executor.eventHandler.OnDamageTaken(this, damageSource, amount);
        if (unitHealth <= 0)
        {
            executor.eventHandler.TickUp -= this.OnTickUp;
            executor.eventHandler.OnUnitDeath(this);
            executor.timeline.AddTimelineEvent(new TimelineDeath(globalObjectId));
        }
    }

    /// <summary>
    /// Event subscriber.
    /// Checks if this is damageTarget.
    /// If true, raises modified TakeDamage event.
    /// </summary>
    public virtual void OnDamageDealt(BattleUnit damageSource, BattleUnit damageTarget, int amount)
    {
        if (damageTarget == this)
        {
            TakeDamage(damageSource, amount);
        }
    }

    /// <summary>
    /// Event subscriber.
    /// Checks if this BattleUnit is dead.
    /// Checks if currentTarget is dead.
    /// </summary>
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

    /// <summary>
    /// Sets currentTarget for this BattleUnit.
    /// </summary>
    public virtual void LookForward()
    {
        currentTarget = BUnitHelperFunc.GetClosestEnemy(this) ?? BUnitHelperFunc.GetClosestEnemy(this);
        executor.timeline.AddTimelineEvent(new TimelineTarget(globalObjectId, currentTarget.globalObjectId));
        Debug.Log(objectName + " (" + globalObjectId + ") targeting "
            + currentTarget.objectName + " (" + currentTarget.globalObjectId + ")");
    }

}
