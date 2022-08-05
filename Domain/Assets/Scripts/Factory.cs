using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    public BattleExecutor executor;

    public Factory(BattleExecutor exec)
    {
        executor = exec;
    }

    //***************** Object Constructor *******************
    public virtual IBattleObject NewObject(int side, string name)
    {
        IBattleObject output = new BattleObject(executor, side, name);

        executor.eventHandler.TickUp += output.Behavior.OnTickUp;

        return output;
    }

    //***************** Unit Constructor *******************
    public virtual IBattleUnit NewUnit(int side, UnitRuntimeData data, int tileId)
    {
        IBattleUnit output = new BattleUnit(executor, side, data, tileId);
        output.Behavior = GetUnitBehavior(output);
        output.Actions = GetUnitActions(output);
        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        EventSubscriber.Subscribe(executor, output.Behavior, data.baseData.eventSubscriptions);
        return output;
    }

    public UnitBehavior GetUnitBehavior(IBattleUnit unit)
    {
        switch (unit.UnitData.baseData.unitId)
        {
            case 0:
                return new AliceBehavior(unit);
            case 1:
                return new UnitBehavior(unit);
            case 2:
                return new JoeBehavior(unit);
            case 3:
                return new DoeBehavior(unit);
        }
        return null;
    }

    public BattleUnitActions GetUnitActions(IBattleUnit unit)
    {
        switch (unit.UnitData.baseData.unitId)
        {
            default:
                return new BattleUnitActions(unit);
        }
    }

    //***************** Status Constructor *******************
    public virtual IBattleStatus NewStatus(StatusType type, IBattleUnit host,
        IBattleUnit source, SimpleStatusData data)
    {
        IBattleStatus output = new BattleStatus(executor, type.ToString(), host, source, data);
        output.Behavior = GetStatusBehavior(type, output);
        output.Actions = GetStatusActions(output);

        host.StatusList.Add(output);

        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        executor.eventHandler.UnitDeath += output.Behavior.OnUnitDeath;

        output.Behavior.OnSpawn();

        return output;
    }

    public StatusBehavior GetStatusBehavior(StatusType type, IBattleStatus status)
    {
        switch (type)
        {
            case StatusType.Burn:
                return new StatusBurnBehavior(status);
            case StatusType.AttackMod:
                return new StatusAttackModifyBehavior(status);
        }
        return null;
    }

    public BattleStatusActions GetStatusActions(IBattleStatus status)
    {
        /*
        switch (status.ObjectName)
        {
            
        }
        */
        return new BattleStatusActions(status);
    }

    //***************** Projectile Constructor *******************
    public virtual IBattleProjectile NewProjectile(IBattleUnit source,
        int index, IBattleUnit target)
    {
        IBattleProjectile output = new BattleProjectile(executor, source, index, target);
        ProjectileInit(output);
        return output;
    }

    public virtual IBattleProjectile NewProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        IBattleProjectile output = new BattleProjectile(executor, source, index, target);
        ProjectileInit(output);
        return output;
    }

    public virtual void ProjectileInit(IBattleProjectile output)
    {
        output.Behavior = GetProjectileBehavior(output);
        output.Actions = GetProjectileActions(output);
        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        executor.eventHandler.UnitDeath += output.Behavior.OnUnitDeath;
    }

    public BasicProjectileBehavior GetProjectileBehavior(IBattleProjectile projectile)
    {
        switch (projectile.ObjectName)
        {
            case "JoeSkill":
                return new JoeFireAOEBehavior(projectile);
            case "DoeSkill":
                return new DoeHealAOEBehavior(projectile);
            default:
                return new BasicProjectileBehavior(projectile);
        }
        //return null;
    }

    public BattleProjectileActions GetProjectileActions(IBattleProjectile projectile)
    {
        return null;
    }
}
