using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeSkillBP : BattleProjectile
{
    public JoeSkillBP(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side, source, index, targetLocation)
    {

    }

    public override void ProjectileEffect()
    {
        float radius = 3;
        List<BattleUnit> targets = new List<BattleUnit>();
        foreach (BattleUnit enemy in executor.GetEnemyUnits(source))
        {
            if (Vector3.Distance(enemy.position, targetLocation) <= radius)
            {
                Debug.Log("JOE BURN");
                targets.Add(enemy);
            }
        }
        foreach (BattleUnit enemy in targets)
        {
            enemy.statusList.Add(new BattleStatusBurn(enemy, source, sourceAttack * 2, 3));
            executor.timeline.AddTimelineEvent(new TimelineAddStatus(enemy.globalObjectId, 1));
        }

        executor.timeline.AddTimelineEvent(new TimelineProjectile(source.globalObjectId,
            targetLocation, targetLocation, 2));

    }
}
