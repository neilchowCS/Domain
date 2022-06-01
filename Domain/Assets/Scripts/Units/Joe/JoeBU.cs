using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoeTargetingExtension;

public class JoeBU : BattleUnit
{
    public JoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side, unitData)
    {

    }

    public JoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void TickUpAttack()
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
                if (!isMoving && TargetInRange())
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

    public override void SpawnProjectile(int i)
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
                    x = new JoeSkillBP(executor, side, this, i, this.GetAoeLocation(3, 0));
                    break;
            }
        }
    }
}
