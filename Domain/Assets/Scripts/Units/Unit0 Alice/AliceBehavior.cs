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
            unit.Executor.factory.NewStatus("Attack Modify Status", unit, unit,
                new SimpleStatusData(-1f, true, .25f));
        }
        SpawnProjectile(i);
        unit.Actions.SetMana(0);
        unit.AttackState = AttackStates.inBackswing;
        unit.AttackTimer = unit.UnitData.baseData.attackDataList[i].backswing;
    }
}
