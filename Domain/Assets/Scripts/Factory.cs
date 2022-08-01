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
    public virtual IBattleStatus NewStatus(string name, IBattleUnit host,
        IBattleUnit source, SimpleStatusData data)
    {
        IBattleStatus output = new BattleStatus(executor, name, host, source, data);
        output.Behavior = GetStatusBehavior(output);
        output.Actions = GetStatusActions(output);

        host.StatusList.Add(output);

        executor.eventHandler.UnitDeath += output.Behavior.OnUnitDeath;

        output.Behavior.OnSpawn();

        return output;
    }

    public static StatusBehavior GetStatusBehavior(IBattleStatus status)
    {
        switch (status.ObjectName)
        {
            case "Burn Status":
                return new StatusBurnBehavior(status);
            case "Attack Modify Status":
                return new StatusAttackModifyBehavior(status);
        }
        return null;
    }

    public static BattleStatusActions GetStatusActions(IBattleStatus status)
    {
        /*
        switch (status.ObjectName)
        {
            
        }
        */
        return new BattleStatusActions(status);
    }

    //***************** Projectile Constructor *******************
    public virtual IBattleProjectile NewProjectile()
    {
        //IBattleProjectile output = new BattleProjectile();

        return null;
    }
}
