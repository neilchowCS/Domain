using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationProjectileBehavior : BasicProjectileBehavior
{
    public LocationProjectileBehavior(IBattleProjectile projectile) : base (projectile)
    {

    }

    public override void OnTickUp()
    {
        projectile.Position = Vector3.MoveTowards(projectile.Position, projectile.TargetLocation,
                projectile.AttackData.speed / TickSpeed.ticksPerSecond);
        if (Vector3.Distance(projectile.Position, projectile.TargetLocation) < 0.0001f)
        {
            Aoe();
            projectile.Actions.Unassign();
        }
    }

    public virtual void Aoe()
    {
        List<IBattleUnit> targets = new List<IBattleUnit>();
        foreach (IBattleUnit enemy in projectile.Executor.GetEnemyUnits(projectile))
        {
            if (Vector3.Distance(enemy.Position, projectile.TargetLocation) <= projectile.AttackData.radius)
            {
                targets.Add(enemy);
            }
        }
        foreach (IBattleUnit enemy in targets)
        {
            ProjectileEffect(enemy);
        }
    }

    public virtual void ProjectileEffect(IBattleUnit target)
    {
        projectile.Executor.DealDamage(projectile.Source, target,
            projectile.UnitState.attack, DamageType.normal);
    }
}
