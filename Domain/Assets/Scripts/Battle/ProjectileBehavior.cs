using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : ObjectBehavior
{
    public IBattleProjectile projectile;

    public ProjectileBehavior(IBattleProjectile projectile)
    {
        this.projectile = projectile;
    }

    public override void OnTickUp()
    {
        if (projectile.AttackData.followTarget && projectile.TargetUnit != null)
        {
            projectile.Position = Vector3.MoveTowards(projectile.Position,
                projectile.TargetUnit.Position,
                projectile.AttackData.speed / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(projectile.Position, projectile.TargetUnit.Position) < 0.0001f)
            {
                ProjectileEffect();
                Unassign();
            }
        }
        else if (projectile.AttackData.projectile && !projectile.AttackData.followTarget
            && projectile.TargetLocation != null)
        {
            projectile.Position = Vector3.MoveTowards(projectile.Position, projectile.TargetLocation,
                projectile.AttackData.speed / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(projectile.Position, projectile.TargetLocation) < 0.0001f)
            {
                ProjectileEffect();
                Unassign();
            }
        }
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == projectile.TargetUnit)
        {
            Unassign();
        }
    }

    public virtual void ProjectileEffect()
    {
        projectile.Executor.DealDamage(projectile.Source, projectile.TargetUnit,
            projectile.UnitState.attack, DamageType.normal);
    }

    public virtual void Unassign()
    {
        projectile.TargetUnit = null;
        projectile.Source.Executor.eventHandler.TickUp -= OnTickUp;
        projectile.Source.Executor.eventHandler.UnitDeath -= OnUnitDeath;
        projectile.Executor.GetAlliedObjects(projectile).Remove(projectile);
    }
}
