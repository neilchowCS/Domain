using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleBehaviorExtension;
using AoeTargetingExtension;

/// <summary>
/// Inherits from BattleObject, IBattleUnit. Unobserved (server side).
/// </summary>
public class BattleUnit : BattleObject, IBattleUnit
{
    public UnitRuntimeData UnitData { get; set; }

    public BattleUnitActions Actions { get; set; }

    public Vector3 Position { get; set; }

    public BattleTile CurrentTile { get; set; }
    public BattleTile TargetTile { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    public bool NeedsCleaning { get; set; } = false;

    public bool IsMoving { get; set; } = false;

    public AttackStates AttackState { get; set; } = AttackStates.idle;
    public float TickOfLastAttack { get; set; } = 0;
    public float AttackTimer { get; set; } = 0;

    public int ManaCounter { get; set; } = 0;

    public List<BattleStatus> StatusList { get; set; }

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side,
        UnitRuntimeData unitData, int tileId)
        : base(exec, side)
    {
        InitBattleUnitValues(exec, side, unitData, tileId);
    }

    private void InitBattleUnitValues(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
    {
        ConstructLogic();

        this.UnitData = unitData;
        ObjectName = unitData.baseData.unitName;
        StatusList = new List<BattleStatus>();

        CurrentTile = exec.battleSpace.tiles[tileId];
        Position = CurrentTile.position;
        CurrentTile.occupied = true;

        EventSubscriber.Subscribe(this, unitData.baseData.eventSubscriptions);
    }

    public virtual void ConstructLogic()
    {
        Behavior = new ObjectBehavior();
        //InitBehavior
        Actions = new BattleUnitActions(this);
    }

    public virtual void TickUpMana()
    {
        ManaCounter++;
        if (ManaCounter >= UnitData.unitTickPerMana.Value)
        {
            UnitData.mana++;
            ManaCounter = 0;
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
        if (CurrentTarget == null)
        {
            LookForward();
        }
        //if not moving, not attacking, and no target in range => initiate movement
        if (!IsMoving && AttackState == AttackStates.idle)
        {
            //target can't be null because it is set at start of function
            if (!TargetInRange())
            {
                LookForward();
                this.PrepareMovement();
                if (TargetTile != CurrentTile)
                {
                    IsMoving = true;
                }
            }
        }
        else if (IsMoving)//move forward, set moving to false at arrival
        {
            this.MoveTowardsNext();
            if (this.TileArrived())
            {
                IsMoving = false;
            }
        }

    }

    public virtual void TickUpAttack()
    {
        //Initiating attack should be in foreswing
        if (AttackState == AttackStates.idle)
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                UseAbility(1);
            }
            else if (!IsMoving && Executor.globalTick - TickOfLastAttack
                >= UnitData.ticksPerAttack && TargetInRange())
            {
                SpawnProjectile(0);
                TickOfLastAttack += UnitData.ticksPerAttack;
                AttackState = AttackStates.inBackswing;
                AttackTimer = UnitData.baseData.attackDataList[0].backswing;
            }
        }else if (AttackState == AttackStates.inBackswing)
        {
            //FIXME check order
            AttackTimer--;
            if (AttackTimer <= 0)
            {
                AttackTimer = 0;
                AttackState = AttackStates.idle;
            }
        }
    }

    public virtual void UseAbility(int i)
    {
        SpawnProjectile(i);
        UnitData.mana = 0;
        AttackState = AttackStates.inBackswing;
        AttackTimer = UnitData.baseData.attackDataList[i].backswing;
    }

    public virtual void SpawnProjectile(int i)
    {
        if (CurrentTarget != null)
        {
            BattleProjectile x = null;
            switch (i)
            {
                case 0:
                    x = new BattleProjectile(Executor, Side, this, i, CurrentTarget);
                    break;
                case 1:
                    x = new AliceSkillBP(Executor, Side, this, i, CurrentTarget);
                    break;
                case 2:
                    x = new JoeSkillBP(Executor, Side, this, i, this.GetAoeLocation(3, 0));
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
    public virtual void TakeDamage(IBattleUnit damageSource, int amount)
    {
        UnitData.health -= amount;
        Executor.eventHandler.OnDamageTaken(this, damageSource, amount);
        if (UnitData.health <= 0)
        {
            Executor.eventHandler.OnUnitDeath(this);
        }
    }

    /// <summary>
    /// Event subscriber.
    /// Checks if this is damageTarget.
    /// If true, raises modified TakeDamage event.
    /// </summary>
    public virtual void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount)
    {
        if (damageTarget == this)
        {
            TakeDamage(damageSource, amount);
        }
    }

    public virtual void ReceiveHeal(IBattleUnit healSource, int amount)
    {
        UnitData.health += amount;

    }

    public virtual void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount)
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
    public virtual void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == this)
        {
            CurrentTile.occupied = false;
            if (TargetTile != null)
            {
                TargetTile.occupied = false;
            }
            if (Side == 0)
            {
                Executor.player0Active.Remove(this);
                Executor.player0Dead.Add(this);
            }
            else
            {
                Executor.player1Active.Remove(this);
                Executor.player1Dead.Add(this);
            }
            NeedsCleaning = true;
        }

        if (deadUnit == CurrentTarget)
        {
            CurrentTarget = null;
        }
    }

    public virtual void OnDamageTaken(IBattleUnit damageTarget, IBattleUnit damageSource, int amount)
    {

    }

    /// <summary>
    /// Sets currentTarget for this BattleUnit.
    /// </summary>
    public virtual void LookForward()
    {
        CurrentTarget = this.GetClosestEnemy();
    }

    public float GetBattleUnitDistance(IBattleUnit otherUnit)
    {
        return Vector3.Distance(Position, otherUnit.Position);
    }

    public float GetTargetDistance()
    {
        return GetBattleUnitDistance(CurrentTarget);
    }

    public bool TargetInRange()
    {
        return GetTargetDistance() < UnitData.unitRange.Value;
    }
}
