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

        //executor.eventHandler.TickUp += output.Behavior.OnTickUp;

        return output;
    }

    public void ObservedObjectConstructor(IBattleObject obj, int side, string name)
    {
        obj.Executor = executor;
        obj.Side = side;
        obj.ObjectName = name;
        obj.GlobalObjectId = executor.SetGlobalObjectId();
        executor.GetAlliedObjects(obj).Add(obj);
    }

    //***************** Unit Constructor *******************
    public virtual IBattleUnit NewUnit(int side, UnitRuntimeData data, (int,int) tile)
    {
        BattleUnit output = null;
        int tileX = tile.Item1;
        int tileY = tile.Item2;
        switch (data.baseData.unitId)
        {
            case 1:
                output = new BattleBob(executor, side, data, tileX, tileY);
                break;
            case 2:
                output = new BattleJoe(executor, side, data, tileX, tileY);
                break;
            case 3:
                output = new BattleDoe(executor, side, data, tileX, tileY);
                break;
            default:
                Debug.Log("warning: default battle unit created");
                output = new BattleUnit(executor, side, data, tileX, tileY);
                break;
        }
        //output = new BattleUnit(executor, side, data, tileId);
        //output.Behavior = GetUnitBehavior(output);
        //output.Actions = GetUnitActions(output);
        //executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        //EventSubscriber.Subscribe(executor, output.Behavior, data.baseData.eventSubscriptions);
        return output;
    }

    public virtual IBattleUnit NewObservedUnit(int side, UnitRuntimeData data, (int, int) tile)
    {
        ObservedUnit output =
            GameObject.Instantiate(executor.replayManager.replayUnitPrefabs[data.baseData.unitId],
            executor.hexMap[tile.Item1, tile.Item2].Position,
            (side == 0 ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0)));
        ObservedUnitConstructor(output, side, data, tile.Item1, tile.Item2);
        //output.Behavior = GetUnitBehavior(output);
        //output.Actions = GetObservedUnitActions(output);
        //executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        //EventSubscriber.Subscribe(executor, output.Behavior, data.baseData.eventSubscriptions);

        output.healthBar = GameObject.Instantiate(output.UnitData.baseData.commonRef.healthBarPrefab,
            executor.replayManager.screenOverlayCanvas.transform, false);
        if (output.Side == 1)
        {
            output.healthBar.healthFill.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        output.healthBar.parent = output;
        output.healthBar.ChangePosition();
        output.healthBar.RefreshFill();

        return output;
    }

    /*
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
    */

    /*
    public BattleUnitActions GetUnitActions(BattleUnit unit)
    {
        switch (unit.UnitData.baseData.unitId)
        {
            default:
                return new BattleUnitActions(unit);
        }
    }
    */

    public void ObservedUnitConstructor(IBattleUnit unit, int side,
        UnitRuntimeData unitData, int x, int y)
    {
        ObservedObjectConstructor(unit, side, unitData.baseData.name);
        unit.UnitData = unitData;
        unit.StatusList = new();

        unit.X = x;
        unit.Y = y;
        unit.Timeline = 0;

        executor.hexMap[unit.X, unit.Y].occupant = unit;

        /*
        this.UnitData = unitData;
        StatusList = new();

        Tile = tileId;
        Timeline = 0;
        Position = Executor.mapGraph[Tile].Position;
        Executor.mapGraph[Tile].occupied = true;
        */
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
        //output.Behavior = GetStatusBehavior(type, output);
        output.Actions = GetStatusActions(output);

        host.StatusList.Add(output);

        //executor.eventHandler.TickUp += output.Behavior.OnTickUp;
        //executor.eventHandler.UnitDeath += output.Behavior.OnUnitDeath;

        //output.Behavior.OnSpawn();

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
    //***************** Observed Projectile *******************

    public ObservedProjectile GetObservedProjectile(ObservedProjectile reference, Vector3 spawnPosition, IBattleUnit target, float speed)
    {
        ObservedProjectile output = GameObject.Instantiate(reference, spawnPosition, Quaternion.identity);
        output.target = target;
        output.unitTarget = true;
        output.speed = speed;
        return output;
    }

    public ObservedProjectile GetObservedProjectile(ObservedUnit source, int id)
    {
        ObservedProjectile output = GameObject.Instantiate(
            source.UnitData.baseData.attackDataList[id].projectilePrefab,
            source.Position, Quaternion.identity);
        output.source = source;
        output.projectileId = id;
        output.target = source.CurrentTarget;
        output.unitTarget = true;
        output.speed = source.UnitData.baseData.attackDataList[id].speed;
        
        return output;
    }

    public ObservedProjectile GetObservedProjectile(ObservedProjectile reference, Vector3 spawnPosition, Vector3 targetLocation, float speed)
    {
        ObservedProjectile output = GameObject.Instantiate(reference, spawnPosition, Quaternion.identity);
        output.targetLocation = targetLocation;
        output.unitTarget = false;
        output.speed = speed;
        return output;
    }
}
