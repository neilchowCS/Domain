using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject
{
    public IBattleUnit source;
    protected int sourceAttack;
    //FIXME ???
    protected int sourceGlobalId = -1;
    public IBattleUnit target;
    public Vector3 position;
    public Vector3 targetLocation;
    public UnitAttackDataScriptableObject attackData;

    public BattleProjectile(BattleExecutor exec, int side, IBattleUnit source,
        int index, IBattleUnit target) : base(exec, side, "Default Projectile")
    {
        Executor.eventHandler.UnitDeath += Behavior.OnUnitDeath;
        this.source = source;
        this.position = source.Position;
        attackData = source.UnitData.baseData.attackDataList[index];
        this.target = target;
        sourceAttack = source.UnitData.unitAttack.Value;
        sourceGlobalId = source.GlobalObjectId;


        Executor.GetAlliedObjects(this).Add(this);
    }

    public BattleProjectile(BattleExecutor exec, int side, IBattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side, "Default Projectile")
    {
        this.source = source;
        this.position = source.Position;
        attackData = source.UnitData.baseData.attackDataList[index];
        this.targetLocation = targetLocation;
        sourceAttack = source.UnitData.unitAttack.Value;
        sourceGlobalId = source.GlobalObjectId;

        Executor.GetAlliedObjects(this).Add(this);
    }

    /*
    public override void OnTickUp()
    {
        if (attackData.followTarget && target != null)
        {
            position = Vector3.MoveTowards(position, target.Position,
                attackData.speed / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(position, target.Position) < 0.0001f)
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

    public virtual void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == target)
        {
            Unassign();
        }
    }
    
    public virtual void ProjectileEffect()
    {
        Executor.DealDamage(source, target, sourceAttack, DamageType.normal);
    }

    public virtual void Unassign()
    {
        target = null;
        source.Executor.eventHandler.TickUp -= OnTickUp;
        source.Executor.eventHandler.UnitDeath -= OnUnitDeath;
        Executor.GetAlliedObjects(this).Remove(this);
    }
    */
}