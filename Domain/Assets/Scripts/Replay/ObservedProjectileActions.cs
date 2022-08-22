using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedProjectileActions : BattleProjectileActions
{
    public ObservedProjectileActions(IBattleProjectile projectile) : base (projectile)
    {

    }

    public override void Unassign()
    {
        base.Unassign();
        GameObject.Destroy(projectile.GetGameObject());
    }
}
