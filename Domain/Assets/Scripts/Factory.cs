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
        IBattleObject output = new BattleObject(executor, BattleObjectType.Normal, side, name);

        //executor.eventHandler.TickUp += output.Behavior.OnTickUp;

        return output;
    }

    //***************** Unit Constructor *******************
    public virtual IBattleUnit NewUnit(int side, UnitRuntimeData data, (int, int) tile)
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

        return output;
    }

    public virtual IBattleUnit NewObservedUnit(int side, UnitRuntimeData data, (int, int) tile)
    {
        ObservedUnit output =
            GameObject.Instantiate(executor.replayManager.replayUnitPrefabs[data.baseData.unitId],
            executor.hexMap[tile.Item1, tile.Item2].Position,
            (side == 0 ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0)));
        output.Initialize(executor, side, data, tile.Item1, tile.Item2);

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

    //***************** Status Constructor *******************

    public virtual ObservedStatusBurn NewObservedStatusBurn(IBattleObject source,
        IBattleUnit host, int duration, int dmgPerTick)
    {
        ObservedStatusBurn burn = GameObject.Instantiate(host.UnitData.baseData.commonRef.observedBurn);
        burn.Initialize(executor, source, host, duration, dmgPerTick);
        return burn;
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
