using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeHealAOEBehavior : LocationProjectileBehavior
{
    public DoeHealAOEBehavior(IBattleProjectile projectile) : base(projectile)
    {

    }

    public override void Aoe()
    {
        List<IBattleUnit> targets = new List<IBattleUnit>();
        foreach (IBattleUnit ally in projectile.Executor.GetAlliedUnits(projectile))
        {
            if (Vector3.Distance(ally.Position, projectile.TargetLocation) <= projectile.AttackData.radius)
            {
                targets.Add(ally);
            }
        }
        foreach (IBattleUnit ally in targets)
        {
            ProjectileEffect(ally);
        }
    }

    public override void ProjectileEffect(IBattleUnit ally)
    {
        projectile.Executor.ApplyHeal(projectile.Source, ally,
            (int)(projectile.UnitState.attack * projectile.AttackData.value0));
        /*
        float radius = 3;
        List<BattleUnit> targets = new List<BattleUnit>();
        foreach (BattleUnit ally in Executor.GetAlliedUnits(source))
        {
            if (Vector3.Distance(ally.Position, targetLocation) <= radius)
            {
                targets.Add(ally);
                Debug.Log(ally.ObjectName);
            }
        }
        foreach (BattleUnit ally in targets)
        {
            Executor.ApplyHeal(source, ally, (int)(sourceAttack * 1.5f));
        }
        */
    }
}
