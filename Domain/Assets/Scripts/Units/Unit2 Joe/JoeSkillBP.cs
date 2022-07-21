using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeSkillBP : BattleProjectile
{
    public JoeSkillBP(BattleExecutor exec, int side, IBattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side, source, index, targetLocation)
    {

    }
    /*
    public override void ProjectileEffect()
    {
        float radius = 3;
        List<BattleUnit> targets = new List<BattleUnit>();
        foreach (BattleUnit enemy in Executor.GetEnemyUnits(source))
        {
            if (Vector3.Distance(enemy.Position, targetLocation) <= radius)
            {
                targets.Add(enemy);
            }
        }
        foreach (BattleUnit enemy in targets)
        {
            enemy.StatusList.Add(new BattleStatusBurn(enemy, source, sourceAttack * 2, 3));
        }

    }
    */
}
