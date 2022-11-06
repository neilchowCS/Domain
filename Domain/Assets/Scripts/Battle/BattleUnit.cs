using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorExtension;
using AoeTargetingExtension;

/// <summary>
/// Inherits from BattleObject, IBattleUnit. Unobserved (server side).
/// </summary>
public class BattleUnit : BattleObject, IBattleUnit
{
    public UnitRuntimeData UnitData { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public int Tile { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    public List<IBattleStatus> StatusList { get; set; }

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side,
        UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData.baseData.unitName)
    {
        this.UnitData = unitData;
        StatusList = new();

        Tile = tileId;
        Timeline = 0;
        Position = Executor.mapGraph[Tile].Position;
        Executor.mapGraph[Tile].occupied = true;
    }

    public virtual void ModifyHealth(int amount, DamageType damageType, IBattleUnit source) {
        UnitData.health += amount;
    }

    public virtual void ModifyMana(int amount) {
        UnitData.mana += amount;
    }

    public virtual void PerformAction()
    {
        bool hasMoved = false;
        bool hasAttacked = false;
        PerformMovement(ref hasMoved);
        PerformAttack(ref hasAttacked);
        if (hasMoved && !hasAttacked)
        {
            Timeline = Executor.maxTimeline - (Executor.maxTimeline * UnitData.unitRecovery.Value);
        }
        else
        {
            Timeline = Executor.maxTimeline;
        }
    }

    public virtual void PerformMovement(ref bool hasMoved)
    {
        if (CurrentTarget == null)
        {
            this.TargetClosestEnemy();
        }
        //target can't be null because it is set at start of function
        if (!this.TargetInRange())
        {
            this.TargetClosestEnemy();
            MoveTowardsNext();
            hasMoved = true;
        }
    }

    /// <summary>
    /// Move to next tile
    /// </summary>
    public void MoveTowardsNext()
    {
        Executor.mapGraph[Tile].occupied = false;

        Tile = this.GetNextBattleTile();
        Executor.mapGraph[Tile].occupied = true;

        //unit.Position = unit.Executor.mapGraph[unit.Tile].Position;
        Position = Executor.mapGraph[Tile].Position;
    }

    public virtual void PerformAttack(ref bool hasAttacked)
    {
        if (this.TargetInRange())
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                PerformSkill();
                ModifyMana(-UnitData.mana);
            }
            else
            {
                PerformBasic();
                ModifyMana(1);
            }
        }
    }

    public virtual void PerformBasic()
    {
        ActionExtension.DealDamage(this, new() { CurrentTarget },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
            DamageType.normal);
    }

    public virtual void PerformSkill()
    {
        ActionExtension.DealDamage(this, new() { CurrentTarget },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.normal);
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == this)
        {
            Executor.mapGraph[Tile].occupied = false;
        }

        if (deadUnit == CurrentTarget)
        {
            CurrentTarget = null;
        }
    }
}