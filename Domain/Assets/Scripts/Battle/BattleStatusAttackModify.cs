using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusAttackModify : BattleStatus
{
    public bool isPercent;
    public bool isPermanent;
    public float amount;

    public BattleStatusAttackModify(BattleUnit host, float amount, bool percent, bool permanence)
        :base(host.Executor, host.Side, "AttackModifyStatus", host)
    {
        this.host = host;
        isPercent = percent;
        isPermanent = permanence;
        this.amount = amount;

        OnApply();
    }

    public void OnApply()
    {
        //Debug.Log(host.unitData.unitAttack.Value);
        if (isPercent)
        {
            host.UnitData.unitAttack.ModifyMultiplicative(amount);
        }
        else
        {
            host.UnitData.unitAttack.ModifyAdditive(amount);
        }
    }

    public override void OnUnapply()
    {
        if (isPercent)
        {
            host.UnitData.unitAttack.ModifyMultiplicative(-amount);
        }
        else
        {
            host.UnitData.unitAttack.ModifyAdditive(-amount);
        }

        host.StatusList.Remove(this);
    }
}
