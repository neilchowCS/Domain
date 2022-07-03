using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleBehaviorExtension;
using AoeTargetingExtension;

/// <summary>
/// A unit created during battle sim.
/// </summary>
public class BattleUnit : BattleObject
{
    public UnitRuntimeData unitData;

    public Vector3 position;

    public BattleTile currentTile;
    public BattleTile targetTile;

    public BattleUnit currentTarget = null;

    public bool needsCleaning = false;

    public bool isMoving = false;

    public enum AttackStates { idle, inForeswing, inAttack, inBackswing, inChannel };
    public AttackStates attackState = AttackStates.idle;
    public float tickOfLastAttack = 0;
    public float attackTimer = 0;

    public int manaCounter = 0;

    public List<BattleStatus> statusList;

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side)
    {
        InitBattleUnitValues(exec, side, unitData, tileId);
    }

    /// <summary>
    /// BattleUnit constructor for random location
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side)
    {
        InitBattleUnitValues(exec, side, unitData, BUnitHelperFunc.GetSpawnLoc(this).id);
    }

    private void InitBattleUnitValues(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
    {
        this.unitData = unitData;
        objectName = unitData.baseData.unitName;
        statusList = new List<BattleStatus>();

        currentTile = exec.battleSpace.tiles[tileId];
        position = currentTile.position;
        currentTile.occupied = true;

        EventSubscriber.Subscribe(this, unitData.baseData.eventSubscriptions);
    }

    /// <summary>
    /// Event subscriber.
    /// Logic for every tick during battle while running.
    /// </summary>
    public override void OnTickUp()
    {
        //if (executor.IsRunning(side))
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
            manaCounter = 0;
        }
    }

    /// <summary>
    /// Handles movement during OnTickUp().
    /// FIXME bugged movement
    /// </summary>
    /*
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
    */

    public virtual void TickUpMove()
    {
        if (currentTarget == null)
        {
            LookForward();
        }
        //if not moving, not attacking, and no target in range => initiate movement
        if (!isMoving && attackState == AttackStates.idle)
        {
            //target can't be null because it is set at start of function
            if (!TargetInRange())
            {
                LookForward();
                this.PrepareMovement();
                if (targetTile != currentTile)
                {
                    isMoving = true;
                }
            }
        }
        else if (isMoving)//move forward, set moving to false at arrival
        {
            this.MoveTowardsNext();
            if (this.TileArrived())
            {
                isMoving = false;
            }
        }

    }

    public virtual void TickUpAttack()
    {
        //Initiating attack should be in foreswing
        if (attackState == AttackStates.idle)
        {
            if (unitData.mana >= unitData.unitMaxMana.Value)
            {
                UseAbility(1);
            }
            else if (!isMoving && executor.globalTick - tickOfLastAttack
                >= unitData.ticksPerAttack && TargetInRange())
            {
                SpawnProjectile(0);
                tickOfLastAttack += unitData.ticksPerAttack;
                attackState = AttackStates.inBackswing;
                attackTimer = unitData.baseData.attackDataList[0].backswing;
            }
        }else if (attackState == AttackStates.inBackswing)
        {
            //FIXME check order
            attackTimer--;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                attackState = AttackStates.idle;
            }
        }
    }

    public virtual void UseAbility(int i)
    {
        SpawnProjectile(i);
        unitData.mana = 0;
        attackState = AttackStates.inBackswing;
        attackTimer = unitData.baseData.attackDataList[i].backswing;
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
                    x = new JoeSkillBP(executor, side, this, i, this.GetAoeLocation(3, 0));
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

    public virtual void ReceiveHeal(BattleUnit healSource, int amount)
    {
        unitData.health += amount;

    }

    public virtual void OnHealApplied(BattleUnit healSource, BattleUnit healTarget, int amount)
    {
        if (healTarget == this)
        {
            ReceiveHeal(healSource, amount);
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
            currentTile.occupied = false;
            if (targetTile != null)
            {
                targetTile.occupied = false;
            }
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
        currentTarget = this.GetClosestEnemy();
    }

    public float GetBattleUnitDistance(BattleUnit otherUnit)
    {
        return Vector3.Distance(position, otherUnit.position);
    }

    public float GetTargetDistance()
    {
        return GetBattleUnitDistance(currentTarget);
    }

    public bool TargetInRange()
    {
        return GetTargetDistance() < unitData.unitRange.Value;
    }
}
