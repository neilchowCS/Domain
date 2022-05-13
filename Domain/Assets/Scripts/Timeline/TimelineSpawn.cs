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
        ReplayUnit x = GameObject.Instantiate(replayExecutor.replayManager.replayUnitPrefabs[indData.baseData.unitId]).GetComponent<ReplayUnit>();

        replayExecutor.InitProfile(globalSpawnId, indData, side);
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

    
}
