using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeSkillBP : BattleProjectile
{
    public DoeSkillBP(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side, source, index, targetLocation)
    {

    }

    public override void ProjectileEffect()
    {
        float radius = 3;
        List<BattleUnit> targets = new List<BattleUnit>();
        foreach (BattleUnit ally in executor.GetAlliedUnits(source))
        {
            if (Vector3.Distance(ally.position, targetLocation) <= radius)
            {
                targets.Add(ally);
                Debug.Log(ally.objectName);
            }
        }
        foreach (BattleUnit ally in targets)
        {
            executor.ApplyHeal(source, ally, (int)(sourceAttack * 1.5f));
        }
    }
}
