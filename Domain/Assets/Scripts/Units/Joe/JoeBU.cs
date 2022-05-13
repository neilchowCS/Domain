using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeBU : BattleUnit
{
    public JoeBU(BattleExecutor exec, int side, UnitData unitData)
        : base(exec, side, unitData)
    {

    }

    public JoeBU(BattleExecutor exec, int side, UnitData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void TickUpAttack()
    {
        if (backswing <= 0)
        {
            if (unitData.mana >= unitData.unitMaxMana.Value)
            {
                SpawnProjectile(2);
                unitData.mana = 0;
                executor.timeline.AddTimelineEvent(
                new TimelineManaChange(globalObjectId, unitData.mana));
                backswing = unitData.baseData.attackDataList[2].backswing;
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
