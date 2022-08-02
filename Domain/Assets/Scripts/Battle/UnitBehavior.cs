using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleBehaviorExtension;
using AoeTargetingExtension;

public class UnitBehavior : ObjectBehavior
{
    protected readonly IBattleUnit unit;

    public UnitBehavior(IBattleUnit host)
    {
        unit = host;
    }

    //********************* OnTickUp ************************
    public override void OnTickUp()
    {
        TickUpMana();
        TickUpMove();
        TickUpAttack();
    }

    public virtual void TickUpMana()
    {
        unit.ManaCounter++;
        if (unit.ManaCounter >= unit.UnitData.unitTickPerMana.Value)
        {
            //unit.UnitData.mana++;
            unit.Actions.ModifyMana(1);
            unit.ManaCounter = 0;
        }
    }

    public virtual void TickUpMove()
    {
        if (unit.CurrentTarget == null)
        {
            unit.TargetClosestEnemy();
        }
        //if not moving, not attacking, and no target in range => initiate movement
        if (!unit.IsMoving && unit.AttackState == AttackStates.idle)
        {
            //target can't be null because it is set at start of function
            if (!unit.TargetInRange())
            {
                unit.TargetClosestEnemy();
                unit.PrepareMovement();
                if (unit.TargetTile != unit.CurrentTile)
                {
                    unit.IsMoving = true;
                }
            }
        }
        else if (unit.IsMoving)//move forward, set moving to false at arrival
        {
            unit.MoveTowardsNext();
            if (unit.TileArrived())
            {
                unit.IsMoving = false;
            }
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

    public virtual void TickUpAttack()
    {
        //Initiating attack should be in foreswing
        if (unit.AttackState == AttackStates.idle)
        {
            if (unit.UnitData.mana >= unit.UnitData.unitMaxMana.Value)
            {
                UseAbility(1);
            }
            else if (!unit.IsMoving && unit.Executor.globalTick - unit.TickOfLastAttack
                >= unit.UnitData.ticksPerAttack && unit.TargetInRange())
            {
                SpawnProjectile(0);
                unit.TickOfLastAttack += unit.UnitData.ticksPerAttack;
                unit.AttackState = AttackStates.inBackswing;
                unit.AttackTimer = unit.UnitData.baseData.attackDataList[0].backswing;
            }
        }
        else if (unit.AttackState == AttackStates.inBackswing)
        {
            //FIXME check order
            unit.AttackTimer--;
            if (unit.AttackTimer <= 0)
            {
                unit.AttackTimer = 0;
                unit.AttackState = AttackStates.idle;
            }
        }
    }

    public virtual void UseAbility(int i)
    {
        SpawnProjectile(i);
        //unit.UnitData.mana = 0;
        unit.Actions.SetMana(0);
        unit.AttackState = AttackStates.inBackswing;
        unit.AttackTimer = unit.UnitData.baseData.attackDataList[i].backswing;
    }

    public virtual void SpawnProjectile(int i)
    {
        if (unit.CurrentTarget != null)
        {
            unit.Executor.factory.NewProjectile(unit, i, unit.CurrentTarget);
        }
    }

    //********************* End OnTickUp ************************

    //********************* OnDamageDealt ************************

    /// <summary>
    /// Event subscriber.
    /// Checks if this is damageTarget.
    /// If true, raises modified TakeDamage event.
    /// </summary>
    public override void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount)
    {
        if (damageTarget == unit)
        {
            unit.Actions.TakeDamage(damageSource, amount);
        }
    }

    //********************* End OnDamageDealt ************************

    //********************* OnDamageTaken ************************

    public override void OnDamageTaken(IBattleUnit damageTarget, IBattleUnit damageSource, int amount)
    {

    }

    //********************* End OnDamageTaken ************************

    //********************* OnUnitDeath ************************

    /// <summary>
    /// Event subscriber.
    /// Checks if this BattleUnit is dead.
    /// Checks if currentTarget is dead.
    /// </summary>
    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == unit)
        {
            unit.CurrentTile.occupied = false;
            if (unit.TargetTile != null)
            {
                unit.TargetTile.occupied = false;
            }
            if (unit.Side == 0)
            {
                unit.Executor.player0Active.Remove(unit);
                unit.Executor.player0Dead.Add(unit);
            }
            else
            {
                unit.Executor.player1Active.Remove(unit);
                unit.Executor.player1Dead.Add(unit);
            }
            unit.NeedsCleaning = true;
        }

        if (deadUnit == unit.CurrentTarget)
        {
            unit.CurrentTarget = null;
        }
    }

    //********************* End OnUnitDeath ************************

    //********************* OnHealApplied ************************

    public override void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount)
    {
        if (healTarget == unit)
        {
            unit.Actions.ReceiveHeal(healSource, amount);
        }
    }

    //********************* End OnHealApplied ************************
}
