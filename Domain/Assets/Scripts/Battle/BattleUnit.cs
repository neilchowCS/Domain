using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;
using AoeTargetingExtension;
using System.Linq;

/// <summary>
/// Inherits from BattleObject, IBattleUnit. Unobserved (server side).
/// </summary>
public class BattleUnit : BattleObject, IBattleUnit
{
    public UnitRuntimeData UnitData { get; set; }
    public AttributeInt UnitSpeed { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public bool IsDead { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    public List<IBattleStatus> StatusList { get; set; }

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side,
        UnitRuntimeData unitData, int tileX, int tileY)
        : base(exec, BattleObjectType.Unit, side, unitData.baseData.unitName)
    {
        this.UnitData = unitData;
        this.UnitSpeed = new(unitData.GenerateSpeed());
        StatusList = new();

        IsDead = false;
        X = tileX;
        Y = tileY;
        Timeline = 0;
        Debug.Log(X + " " + Y);
        Position = Executor.hexMap[X, Y].Position;
        Executor.hexMap[X, Y].occupant = this;
    }

    public virtual void ModifyHealth(int amount, DamageType damageType)
    {
        UnitData.health += amount;
    }

    public virtual void ModifyMana(int amount)
    {
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
        (int, int) temp = (X, Y);

        Executor.hexMap[X, Y].occupant = null;

        (int, int) newTile = this.GetNextBattleTile();
        //FIXME
        X = newTile.Item1;
        Y = newTile.Item2;
        Executor.hexMap[newTile.Item1, newTile.Item2].occupant = this;

        //unit.Position = unit.Executor.mapGraph[unit.Tile].Position;
        Position = Executor.hexMap[X, Y].Position;

        Executor.logger.AddMovement(this, temp.Item1, temp.Item2);
    }

    public virtual void PerformAttack(ref bool hasAttacked)
    {
        if (this.TargetInRange())
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                Executor.logger.AddAttack(this, CurrentTarget, AbilityType.Skill);
                PerformSkill();
                ModifyMana(-UnitData.mana);
            }
            else
            {
                Executor.logger.AddAttack(this, CurrentTarget, AbilityType.Basic);
                PerformBasic();
                ModifyMana(1);
            }
            hasAttacked = true;
        }
    }

    public virtual void PerformBasic()
    {
        /*
        Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(this, new() { CurrentTarget },
           (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
           DamageType.normal, AbilityType.Basic).Cast<IEventTrigger>().ToList()
        );
        */
        Executor.eventManager.InitiateTriggers(ActionExtension.ActionExtension.ProcessDamage(this, new() { CurrentTarget },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
            DamageType.normal, AbilityType.Basic));
    }

    public virtual void PerformSkill()
    {
        Executor.eventManager.InitiateTriggers(ActionExtension.ActionExtension.ProcessDamage(this, new() { CurrentTarget },
           (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
           DamageType.normal, AbilityType.Skill));
    }
}