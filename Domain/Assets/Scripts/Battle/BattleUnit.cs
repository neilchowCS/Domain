using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unit created during battle sim.
/// </summary>
public class BattleUnit : BattleObject
{
    public UnitData unitData;
    public int unitHealth;

    public Vector3 position;

    public BattleTile currentTile;
    public BattleTile targetTile;

    public BattleUnit currentTarget = null;

    public bool needsCleaning = false;

    public enum MoveStates { noTarget, movingToTile, tileArrived, inRange, stopped };
    /// <summary>
    /// noTarget, movingToTile, tileArrived, inRange, stopped.
    /// </summary>
    public MoveStates moveState = MoveStates.noTarget;

    public float attackCounter = 0;

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, UnitData unitData)
        : base(exec, side)
    {
        this.unitData = unitData;
        objectName = unitData.baseData.unitName;
        unitHealth = unitData.unitHealth;

        currentTile = BUnitHelperFunc.GetSpawnLoc(this);
        position = currentTile.position;
        currentTile.occupied = true;

        //currentTarget = null;

        //moveState = MoveStates.noTarget;

        EventSubscriber.Subscribe(this, unitData.baseData.eventSubscriptions);

        executor.timeline.AddTimelineEvent(new TimelineSpawn(unitData, globalObjectId, side,
            0, position.x, position.y, position.z));
        Debug.Log("Spawned " + objectName + " (" + globalObjectId + ")");
    }

    /// <summary>
    /// Event subscriber.
    /// Logic for every tick during battle while running.
    /// </summary>
    public override void OnTickUp()
    {
        if (executor.IsRunning(side))
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
            BattleMovement.TargetDecision(this);
        }
        else if (moveState == MoveStates.movingToTile)
        {
            BattleMovement.MoveTowardsNext(this);
        }
        else if (moveState == MoveStates.tileArrived)
        {
            BattleMovement.TargetDecision(this);
        }
    }

    /// <summary>
    /// Handles attacking during OnTickUp().
    /// </summary>
    public virtual void TickUpAttack()
    {
        if (attackCounter < 1)
        {
            if (moveState == MoveStates.inRange)
            {
                SpawnProjectile();
                attackCounter = unitData.unitAttackSpeed;
            }
        }
        else
        {
            attackCounter -= 1;
        }
    }

    public virtual void SpawnProjectile()
    {
        if (currentTarget != null)
        {
            BattleProjectile x = new BattleProjectile(executor, side, this, 0, currentTarget);

            executor.playerObjects0.Add(x);
            executor.timeline.AddTimelineEvent(new TimelineProjectile(globalObjectId, currentTarget.globalObjectId));
        }
        
    }

    /// <summary>
    /// Decreases this unit's health.
    /// Raises TakeDamage event.
    /// Checks if this is dead.
    /// If true, raises UnitDeath event.
    /// </summary>
    public virtual void TakeDamage(BattleUnit damageSource, int amount)
    {
        unitHealth -= amount;
        executor.eventHandler.OnDamageTaken(this, damageSource, amount);
        if (unitHealth <= 0)
        {
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
            needsCleaning = true;
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
    /// FIXME uneccessary code?? refactor
    /// </summary>
    public virtual void LookForward()
    {
        currentTarget = BUnitHelperFunc.GetClosestEnemy(this) ?? BUnitHelperFunc.GetClosestEnemy(this);
        executor.timeline.AddTimelineEvent(new TimelineTarget(globalObjectId, currentTarget.globalObjectId));
        Debug.Log(objectName + " (" + globalObjectId + ") targeting "
            + currentTarget.objectName + " (" + currentTarget.globalObjectId + ")");
    }

}
