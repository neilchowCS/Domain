using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeFireAOEBehavior : LocationProjectileBehavior
{
    public JoeFireAOEBehavior(IBattleProjectile projectile) : base(projectile)
    {

    }
    
    public override void ProjectileEffect(IBattleUnit enemy)
    {
        projectile.Executor.factory.NewStatus(StatusType.Burn, enemy, projectile.Source,
            new SimpleStatusData(projectile.AttackData.value1, false,
            projectile.UnitState.attack * projectile.AttackData.value0));
    }
}