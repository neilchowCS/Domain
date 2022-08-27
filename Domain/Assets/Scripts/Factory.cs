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

    public void ObservedObjectConstructor(IBattleObject obj, int side, string name)
    {
        obj.Executor = executor;
        obj.Side = side;
        obj.ObjectName = name;
        executor.GetAlliedObjects(obj).Add(obj);
    }

    //***************** Unit Constructor *******************
    public virtual IBattleUnit NewUnit(int side, UnitRuntimeData data, int tileId)
    {
        BattleUnit output = new BattleUnit(executor, side, data, tileId);
        output.Behavior = GetUnitBehavior(output);
        output.Actions = GetUnitActions(output);
        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        EventSubscriber.Subscribe(executor, output.Behavior, data.baseData.eventSubscriptions);
        return output;
    }

    public virtual IBattleUnit NewObservedUnit(int side, UnitRuntimeData data, int tileId)
    {
        Debug.Log(tileId);
        ObservedUnit output =
            GameObject.Instantiate(executor.replayManager.replayUnitPrefabs[data.baseData.unitId],
            executor.battleSpace.tiles[tileId].Position,
            (side == 0 ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0)));
        ObservedUnitConstructor(output, side, data, tileId);
        output.Behavior = GetUnitBehavior(output);
        output.Actions = GetObservedUnitActions(output);
        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        EventSubscriber.Subscribe(executor, output.Behavior, data.baseData.eventSubscriptions);

        output.healthBar = GameObject.Instantiate(output.UnitData.baseData.commonRef.healthBarPrefab,
            executor.replayManager.screenOverlayCanvas.transform, false);
        if (output.Side == 1)
        {
            output.healthBar.healthFill.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        output.healthBar.parent = output;
        output.healthBar.ChangePosition();

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

    public BattleUnitActions GetUnitActions(BattleUnit unit)
    {
        switch (unit.UnitData.baseData.unitId)
        {
            default:
                return new BattleUnitActions(unit);
        }
    }

    public void ObservedUnitConstructor(IBattleUnit unit, int side,
        UnitRuntimeData unitData, int tileId)
    {
        ObservedObjectConstructor(unit, side, unitData.baseData.name);
        unit.UnitData = unitData;
        unit.StatusList = new();

        unit.CurrentTile = executor.battleSpace.tiles[tileId];
        unit.CurrentTile.occupied = true;
    }

    public ObservedUnitActions GetObservedUnitActions(ObservedUnit unit)
    {
        switch (unit.UnitData.baseData.unitId)
        {
            default:
                return new ObservedUnitActions(unit);
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

    public virtual void OProjectileInit(IBattleProjectile output)
    {
        output.Behavior = GetProjectileBehavior(output);
        output.Actions = GetObservedProjectileActions(output);
        executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        executor.eventHandler.UnitDeath += output.Behavior.OnUnitDeath;
    }

    public virtual IBattleProjectile NewObservedProjectile(IBattleUnit source,
        int index, IBattleUnit target)
    {
        IBattleProjectile output =
            GameObject.Instantiate(source.UnitData.baseData.attackDataList[index].projectilePrefab,
            source.Position, Quaternion.identity);
        ObservedProjectileConstructor(output, source, index);
        output.TargetUnit = target;
        OProjectileInit(output);
        return output;
    }

    public virtual IBattleProjectile NewObservedProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        IBattleProjectile output =
            GameObject.Instantiate(source.UnitData.baseData.attackDataList[index].projectilePrefab,
            source.Position, Quaternion.identity);
        OProjectileInit(output);
        output.TargetLocation = target;
        return output;
    }

    public void ObservedProjectileConstructor(IBattleProjectile projectile, IBattleUnit source,
        int index)
    {
        ObservedObjectConstructor(projectile, source.Side,
            source.UnitData.baseData.attackDataList[index].Name);
        projectile.Source = source;
        projectile.Position = source.Position;
        projectile.AttackData = source.UnitData.baseData.attackDataList[index];
        projectile.UnitState = new FixedUnitState(source);
        projectile.SourceGlobalId = source.GlobalObjectId;
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
        return new BattleProjectileActions(projectile);
    }

    public BattleProjectileActions GetObservedProjectileActions(IBattleProjectile projectile)
    {
        return new ObservedProjectileActions(projectile);
    }
}
