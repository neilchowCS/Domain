using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject
{
    public BattleUnit source;
    protected int sourceAttack;
    //FIXME ???
    protected int sourceGlobalId = -1;
    public BattleUnit target;
    public Vector3 position;
    public Vector3 targetLocation;
    public UnitAttackDataScriptableObject attackData;

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, BattleUnit target) : base(exec, side)
    {
        executor.eventHandler.UnitDeath += OnUnitDeath;
        this.source = source;
        this.position = source.position;
        attackData = source.unitData.baseData.attackDataList[index];
        this.target = target;
        sourceAttack = source.unitData.unitAttack.Value;
        sourceGlobalId = source.globalObjectId;


        executor.GetAlliedObjects(this).Add(this);
        executor.timeline.AddTimelineEvent(
                new TimelineProjectile(sourceGlobalId, target.globalObjectId, index));
    }

    public BattleProjectile(BattleExecutor exec, int side, BattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side)
    {
        this.source = source;
        this.position = source.position;
        attackData = source.unitData.baseData.attackDataList[index];
        this.targetLocation = targetLocation;
        sourceAttack = source.unitData.unitAttack.Value;
        sourceGlobalId = source.globalObjectId;

        executor.GetAlliedObjects(this).Add(this);
        executor.timeline.AddTimelineEvent(
                new TimelineProjectile(sourceGlobalId, targetLocation, index));
    }

    public override void OnTickUp()
    {
        if (/*attackData.projectile &&*/ attackData.followTarget && target != null)
        {
            position = Vector3.MoveTowards(position, target.position,
                attackData.speed / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(position, target.position) < 0.0001f)
            {
                ProjectileEffect();
                Unassign();
            }
        }
        else if (attackData.projectile && !attackData.followTarget && targetLocation != null)
        {
            position = Vector3.MoveTowards(position, targetLocation,
                attackData.speed / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(position, targetLocation) < 0.0001f)
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
            Unassign();
        }
    }

    public virtual void ProjectileEffect()
    {
        executor.DealDamage(source, target, sourceAttack);
    }

    public virtual void Unassign()
    {
        target = null;
        source.executor.eventHandler.TickUp -= OnTickUp;
        source.executor.eventHandler.UnitDeath -= OnUnitDeath;
        executor.GetAlliedObjects(this).Remove(this);
    }
}