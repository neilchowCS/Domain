using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBU : BattleUnit
{
    /// <summary>
    /// Constructor for Alice BattleUnit. (Unobserved)
    /// </summary>
    public AliceBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void UseAbility(int i)
    {
        if (i == 1)
        {
            StatusList.Add(new BattleStatusAttackModify(this, .25f, true, true));
        }
        SpawnProjectile(i);
        UnitData.mana = 0;
        AttackState = AttackStates.inBackswing;
        AttackTimer = UnitData.baseData.attackDataList[i].backswing;
    }

    public override void SpawnProjectile(int i)
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
            }
        }
    }
}
