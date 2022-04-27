using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusAttackModify : BattleStatus
{
    public BattleUnit host;
    public bool isPercent;
    public bool isPermanent;
    public float amount;

    public BattleStatusAttackModify(BattleUnit host, float amount, bool percent, bool permanence)
        :base(host.executor, host.side, 0, "Attack modify")
    {
        this.host = host;
        isPercent = percent;
        isPermanent = permanence;
        this.amount = amount;

        OnApply();
    }

    public void OnApply()
    {
        if (isPercent)
        {
            host.unitData.unitAttack.ModifyMultiplicative(amount);
        }
        else
        {
            host.unitData.unitAttack.ModifyAdditive(amount);
        }
    }

    public void OnUnapply()
    {
        if (isPercent)
        {
            host.unitData.unitAttack.ModifyMultiplicative(-amount);
        }
        else
        {
            host.unitData.unitAttack.ModifyAdditive(-amount);
        }

        host.statusList.Remove(this);
    }
}
