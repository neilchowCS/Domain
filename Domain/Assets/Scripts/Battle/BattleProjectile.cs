using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject
{
    public BattleUnit source;
    protected int sourceAttack;
    protected int sourceGlobalId;
    public BattleUnit target;
    public Vector3 position;
    public Vector3 targetLocation;
    public UnitAttackDataScriptableObject attackData;

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, BattleUnit target):base(exec, side)
    {
        executor.eventHandler.UnitDeath += OnUnitDeath;
        this.source = source;
        this.position = source.position;
        attackData = source.unitData.baseData.attackDataList[index];
        this.target = target;
        sourceAttack = source.unitData.unitAttack;
        sourceGlobalId = source.globalObjectId;
    }

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 target) : base(exec, side)
    {
        this.source = source;
        attackData = source.unitData.baseData.attackDataList[index];
        this.targetLocation = target;
        sourceAttack = source.unitData.unitAttack;
    }

    public override void OnTickUp()
    {
        if (attackData.projectile && attackData.followTarget && target != null)
        {
            position = Vector3.MoveTowards(position, target.position,
                attackData.speed / TickSpeed.ticksPerSecond);
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
        DealDamage(target, sourceAttack);
    }

    /// <summary>
    /// Raises DealDamage event.
    /// Deals damage equal to unit's attack to damageTarget.
    /// </summary>
    public virtual void DealDamage(BattleUnit damageTarget, int amount)
    {
        executor.eventHandler.OnDamageDealt(source, damageTarget, amount);
        executor.timeline.AddTimelineEvent(new TimelineDamageDealt(sourceGlobalId,
            damageTarget.globalObjectId, amount));

    }

    public virtual void Unassign()
    {
        target = null;
        source.executor.eventHandler.TickUp -= OnTickUp;
        source.executor.eventHandler.UnitDeath -= OnUnitDeath;
        source.executor.playerObjects0.Remove(this);
    }
}