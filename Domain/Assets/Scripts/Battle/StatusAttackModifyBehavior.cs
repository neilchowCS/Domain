using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class StatusAttackModifyBehavior : StatusBehavior
{
    private float lifetime = 0;

    public StatusAttackModifyBehavior(IBattleStatus host) : base(host)
    {

    }

    public override void OnTickUp()
    {
        if (status.StatusData.duration >= 0)
        {
            lifetime++;
            if (lifetime >= TickSpeed.ticksPerSecond * status.StatusData.duration)
            {
                OnUnapply();
            }
        }
    }

    public override void OnSpawn()
    {
        //Debug.Log(host.unitData.unitAttack.Value);
        if (status.StatusData.isPercent)
        {
            status.Host.UnitData.unitAttack.ModifyMultiplicative(status.StatusData.value0);
        }
        else
        {
            status.Host.UnitData.unitAttack.ModifyAdditive(status.StatusData.value0);
        }
    }

    public override void OnUnapply()
    {
        if (status.StatusData.isPercent)
        {
            status.Host.UnitData.unitAttack.ModifyMultiplicative(-status.StatusData.value0);
        }
        else
        {
            status.Host.UnitData.unitAttack.ModifyAdditive(-status.StatusData.value0);
        }

        base.OnUnapply();
    }
}
*/