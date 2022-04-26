using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0 = spawnId
/// 1 = globalSpawnId
/// 2 = side
/// 3 = spawnTileId
/// 4 = spawnPosX
/// 5 = spawnPosY
/// 6 = spawnPosZ
/// </summary>
public class TimelineSpawn : TimelineEvent
{
    public UnitIndependentData indData;
    public int globalSpawnId;
    public int side;
    public int spawnTileId;

    public TimelineSpawn(UnitIndependentData indData,
        int globalSpawnId,
        int side,
        int spawnTileId)
    {
        this.indData = indData;
        this.globalSpawnId = globalSpawnId;
        this.side = side;
        this.spawnTileId = spawnTileId;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit x = GameObject.Instantiate(replayExecutor.replayManager.prefabs[indData.baseData.unitId]).GetComponent<ReplayUnit>();

        InitProfile(replayExecutor);
        InitUnit(replayExecutor, x);
    }

    public void InitUnit(ReplayExecutor executor, ReplayUnit unit)
    {
        unit.transform.position = new Vector3(executor.tiles[spawnTileId].transform.position.x,
            executor.tiles[spawnTileId].transform.position.y + .5f,
            executor.tiles[spawnTileId].transform.position.z);
        if (side == 1)
        {
            unit.transform.rotation = Quaternion.Euler(0, -90, 0);
            unit.side = side;
        }
        executor.replayObjects.Add(unit);
        executor.replayUnits.Add(unit);
        unit.globalId = globalSpawnId;
        unit.unitData = new UnitData(indData);
    }

    public void InitProfile(ReplayExecutor executor)
    {
        ReplayProfile y = GameObject.Instantiate(executor.replayManager.profile).GetComponent<ReplayProfile>();
        executor.profiles.Add(y);
        y.globalId = globalSpawnId;
        y.SetName(indData.baseData.unitName);
        y.SetImage(indData.baseData.unitSprite);
        y.transform.SetParent(executor.replayManager.profileParent.transform, false);
        if (side == 1)
        {
            y.transform.localPosition = new Vector3(-y.transform.localPosition.x,
                y.transform.localPosition.y - (executor.GetNumberOfSide(1)) * 75,
                y.transform.localPosition.z);
        }
        else
        {
            y.transform.localPosition = new Vector3(y.transform.localPosition.x,
                y.transform.localPosition.y - (executor.GetNumberOfSide(0)) * 75,
                y.transform.localPosition.z);
        }
    }
}
