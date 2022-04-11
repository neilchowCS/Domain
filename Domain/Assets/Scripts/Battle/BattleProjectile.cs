using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject
{
    public BattleUnit source;
    public BattleUnit target = null;
    public Vector3 position;
    public Vector3 targetLocation;
    public ProjectileDataScriptableObject projectile;

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, BattleUnit target):base(exec, side)
    {
        source.executor.eventHandler.UnitDeath += OnUnitDeath;
        this.source = source;
        this.position = source.position;
        projectile = source.unitData.baseData.projectileData[index];
        this.target = target;
    }

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 target) : base(exec, side)
    {
        this.source = source;
        projectile = source.unitData.baseData.projectileData[index];
        this.targetLocation = target;
    }

    public override void OnTickUp()
    {
        if (projectile.projectile && projectile.followTarget)
        {
            position = Vector3.MoveTowards(position, target.position,
                projectile.speed * 1f / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(position, target.position) < 0.00001f)
            {
                source.ProjectileHit(target);
                Unassign();
            }
        }
    }

    public virtual void OnUnitDeath(BattleUnit deadUnit)
    {
        if (deadUnit == target)
        {
            target = null;
            source.executor.eventHandler.TickUp -= OnTickUp;
            source.executor.eventHandler.UnitDeath -= OnUnitDeath;
        }
    }

    public void Unassign()
    {
        target = null;
        source.executor.eventHandler.TickUp -= OnTickUp;
        source.executor.eventHandler.UnitDeath -= OnUnitDeath;
        source.executor.playerObjects0.Remove(this);
    }
}