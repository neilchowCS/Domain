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

    public float attackTimer = 0;
    public bool firstAttack = true;
    public float backswing = 0;
    public int manaCounter = 0;

    public List<BattleStatus> statusList;

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, UnitData unitData, int tileId)
        : base(exec, side)
    {
        InitBattleUnitValues(exec, side, unitData, tileId);
    }

    /// <summary>
    /// BattleUnit constructor for random location
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, UnitData unitData)
        : base(exec, side)
    {
        InitBattleUnitValues(exec, side, unitData, BUnitHelperFunc.GetSpawnLoc(this).id);
    }

    private void InitBattleUnitValues(BattleExecutor exec, int side, UnitData unitData, int tileId)
    {
        this.unitData = unitData;
        objectName = unitData.baseData.unitName;
        statusList = new List<BattleStatus>();

        currentTile = exec.battleSpace.tiles[tileId];
        position = currentTile.position;
        currentTile.occupied = true;

        EventSubscriber.Subscribe(this, unitData.baseData.eventSubscriptions);

        executor.timeline.AddInitialSpawn(new TimelineSpawn(unitData.independentData, globalObjectId, side,
            tileId));
    }

    /// <summary>
    /// Event subscriber.
    /// Logic for every tick during battle while running.
    /// </summary>
    public override void OnTickUp()
    {
        if (executor.IsRunning(side))
        {
            TickUpMana();
            TickUpMove();
            TickUpAttack();
        }
    }

    public virtual void TickUpMana()
    {
        manaCounter++;
        if (manaCounter >= unitData.unitTickPerMana.Value)
        {
            unitData.mana++;
            executor.timeline.AddTimelineEvent(
                new TimelineManaChange(globalObjectId, unitData.mana));
            manaCounter = 0;
        }
    }

    /// <summary>
    /// Handles movement during OnTickUp().
    /// FIXME bugged movement
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
        if (backswing <= 0)
        {
            if (unitData.mana >= unitData.unitMaxMana.Value)
            {
                SpawnProjectile(1);
                unitData.mana = 0;
                executor.timeline.AddTimelineEvent(
                new TimelineManaChange(globalObjectId, unitData.mana));
                backswing = unitData.baseData.attackDataList[1].backswing;
                //moveState = MoveStates.noTarget;
                //why does it cease to move when not in range?
                //FIXME
            }
            else if (firstAttack || attackTimer >= 1f / unitData.unitAttackSpeed.Value)
            {
                if (moveState == MoveStates.inRange)
                {
                    SpawnProjectile(0);
                    attackTimer = 0;
                    firstAttack = false;
                    backswing = unitData.baseData.attackDataList[0].backswing;
                }
            }
        }
        else
        {
            backswing--;
        }

        attackTimer += TickSpeed.secondsPerTick;
    }

    public virtual void SpawnProjectile(int i)
    {
        if (currentTarget != null)
        {
            BattleProjectile x = null;
            switch (i)
            {
                case 0:
                    x = new BattleProjectile(executor, side, this, i, currentTarget);
                    break;
                case 1:
                    x = new AliceSkillBP(executor, side, this, i, currentTarget);
                    break;
                case 2:
                    if (this.objectName != "Joe")
                    {
                        Debug.Log("WEEEEWOOOO!!! " + this.objectName);
                    }
                    x = new BattleProjectile(executor, side, this, i, AoeTargeting.GetAoeLocation(this, 6, 0));
                    break;
            }
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
        unitData.health -= amount;
        executor.eventHandler.OnDamageTaken(this, damageSource, amount);
        if (unitData.health <= 0)
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
    }

}
