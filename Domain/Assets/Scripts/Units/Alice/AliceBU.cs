using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBU : BattleUnit
{
    /// <summary>
    /// Constructor for Alice BattleUnit.
    /// </summary>
    /// <param name="exec"> BattleExecutor </param>
    /// <param name="side"> 0 or 1 </param>
    public AliceBU(BattleExecutor exec, int side, UnitData unitData)
        : base(exec, side, unitData)
    {
        
    }

    public AliceBU(BattleExecutor exec, int side, UnitData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void TickUpAttack()
    {
        if (backswing <= 0)
        {
            if (unitData.mana >= unitData.unitMaxMana.Value)
            {
                statusList.Add(new BattleStatusAttackModify(this, .25f, true, true));
                executor.timeline.AddTimelineEvent(new TimelineAddStatus(globalObjectId, 0));
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
}
