using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusAttackModify : BattleStatus
{
    public bool isPercent;
    public bool isPermanent;
    public float amount;

    public BattleStatusAttackModify(BattleExecutor exec, int side, float amount, bool percent, bool permanence)
        :base(exec, side, 0, "Attack modify")
    {
        isPercent = percent;
        isPermanent = permanence;
        this.amount = amount;
    }

    public void OnApply()
    {

    }

    public void OnUnapply()
    {

    }
}
