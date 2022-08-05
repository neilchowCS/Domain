using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileBehavior : ObjectBehavior
{
    public IBattleProjectile projectile;

    public BasicProjectileBehavior(IBattleProjectile projectile)
    {
        this.projectile = projectile;
    }

    public override void OnTickUp()
    {
        if (projectile.TargetUnit != null)
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
        Debug.Log(projectile.Source.ObjectName);
        Debug.Log((int)(projectile.UnitState.attack * projectile.AttackData.value0));
        projectile.Executor.DealDamage(projectile.Source, projectile.TargetUnit,
            (int)(projectile.UnitState.attack * projectile.AttackData.value0), DamageType.normal);
    }

    public virtual void Unassign()
    {
        projectile.TargetUnit = null;
        projectile.Source.Executor.eventHandler.TickUp -= OnTickUp;
        projectile.Source.Executor.eventHandler.UnitDeath -= OnUnitDeath;
        projectile.Executor.GetAlliedObjects(projectile).Remove(projectile);
    }
}
