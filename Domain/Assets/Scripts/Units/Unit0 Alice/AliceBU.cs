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
    public AliceBU(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side, unitData)
    {
        
    }

    public AliceBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void UseAbility(int i)
    {
        if (i == 1)
        {
            statusList.Add(new BattleStatusAttackModify(this, .25f, true, true));
        }
        SpawnProjectile(i);
        unitData.mana = 0;
        attackState = AttackStates.inBackswing;
        attackTimer = unitData.baseData.attackDataList[i].backswing;
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
                    x = new AliceSkillBP(executor, side, this, i, currentTarget);
                    break;
            }
        }
    }
}
