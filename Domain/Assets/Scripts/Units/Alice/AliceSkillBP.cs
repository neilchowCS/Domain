using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceSkillBP : BattleProjectile
{
    public AliceSkillBP(BattleExecutor exec, int side, BattleUnit source,
        int index, BattleUnit target) : base(exec, side, source, index, target)
    {

    }

    public override void ProjectileEffect()
    {
        int damageAmount = sourceAttack * 5;
        Debug.Log(damageAmount);
        DealDamage(target, damageAmount);
    }
}
