using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject
{
    public BattleUnit source;
    private int sourceAttack;
    private int sourceGlobalId;
    public BattleUnit target;
    public Vector3 position;
    public Vector3 targetLocation;
    public ProjectileDataScriptableObject projectile;

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, BattleUnit target):base(exec, side)
    {
        executor.eventHandler.UnitDeath += OnUnitDeath;
        this.source = source;
        this.position = source.position;
        projectile = source.unitData.baseData.projectileData[index];
        this.target = target;
        sourceAttack = source.unitData.unitAttack;
        sourceGlobalId = source.globalObjectId;
    }

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 target) : base(exec, side)
    {
        this.source = source;
        projectile = source.unitData.baseData.projectileData[index];
        this.targetLocation = target;
        sourceAttack = source.unitData.unitAttack;
    }

    public override void OnTickUp()
    {
        if (projectile.projectile && projectile.followTarget && target != null)
        {
            position = Vector3.MoveTowards(position, target.position,
                projectile.speed * 1f / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(position, target.position) < 0.00001f)
            {
                ProjectileEffect();
                Unassign();
            }
        }
    }

    public virtual void OnUnitDeath(BattleUnit deadUnit)
    {
        if (deadUnit == target)
        {
            Debug.Log("Target dead");
            Unassign();
        }
    }

    public virtual void ProjectileEffect()
    {
        DealDamage(target);
    }

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public virtual void DealDamage(BattleUnit damageTarget)
    {
        executor.eventHandler.OnDamageDealt(source, damageTarget, sourceAttack);
        executor.timeline.AddTimelineEvent(new TimelineDamageDealt(sourceGlobalId,
            damageTarget.globalObjectId, sourceAttack));

    }

    public void Unassign()
    {
        target = null;
        source.executor.eventHandler.TickUp -= OnTickUp;
        source.executor.eventHandler.UnitDeath -= OnUnitDeath;
        source.executor.playerObjects0.Remove(this);
    }
}