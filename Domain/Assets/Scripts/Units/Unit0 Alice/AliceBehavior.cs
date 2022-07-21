using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBehavior : UnitBehavior
{
    public AliceBehavior(IBattleUnit host): base(host)
    {

    }

    public override void UseAbility(int i)
    {
        if (i == 1)
        {
            unit.StatusList.Add(new BattleStatusAttackModify(unit, .25f, true, true));
        }
        SpawnProjectile(i);
        unit.Actions.SetMana(0);
        unit.AttackState = AttackStates.inBackswing;
        unit.AttackTimer = unit.UnitData.baseData.attackDataList[i].backswing;
    }
}
