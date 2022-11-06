using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBehavior : UnitBehavior
{
    public AliceBehavior(IBattleUnit host) : base(host)
    {

    }
    /*
    public override void QueueSkillCommand()
    {
        base.QueueSkillCommand();
        unit.Executor.factory.NewStatus(StatusType.AttackMod, unit, unit,
            new SimpleStatusData(-1f, true, .25f));

    }
    */
}
