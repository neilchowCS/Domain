using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectileActions
{
    protected readonly IBattleProjectile projectile;

    public BattleProjectileActions(IBattleProjectile projectile)
    {
        this.projectile = projectile;
    }

    public virtual void Unassign()
    {
        projectile.TargetUnit = null;
        projectile.Source.Executor.eventHandler.TickUp -= projectile.Behavior.OnTickUp;
        projectile.Source.Executor.eventHandler.UnitDeath -= projectile.Behavior.OnUnitDeath;
        projectile.Executor.GetAlliedObjects(projectile).Remove(projectile);
    }
}
