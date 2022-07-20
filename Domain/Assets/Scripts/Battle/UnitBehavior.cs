using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehavior : ObjectBehavior
{
    public override void OnTickUp()
    {
        TickUpMana();
        TickUpMove();
        TickUpAttack();
    }

    public virtual void TickUpMana()
    {
        /*
        manaCounter++;
        if (manaCounter >= unitData.unitTickPerMana.Value)
        {
            unitData.mana++;
            manaCounter = 0;
        }
        */
    }

    public virtual void TickUpMove()
    {
        /*
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
        */
    }

    public virtual void TickUpAttack()
    {
        /*
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
        }
        else if (attackState == AttackStates.inBackswing)
        {
            //FIXME check order
            attackTimer--;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                attackState = AttackStates.idle;
            }
        }
        */
    }
}
