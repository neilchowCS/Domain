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
    public UnitData unitData;
    public int globalSpawnId;
    public int side;
    public int spawnTileId;
    public float spawnPosX;
    public float spawnPosY;
    public float spawnPosZ;

    public TimelineSpawn(UnitData unitData,
        int globalSpawnId,
        int side,
        int spawnTileId,
        float spawnPosX,
        float spawnPosY,
        float spawnPosZ)
    {
        this.unitData = unitData;
        this.globalSpawnId = globalSpawnId;
        this.side = side;
        this.spawnTileId = spawnTileId;
        this.spawnPosX = spawnPosX;
        this.spawnPosY = spawnPosY;
        this.spawnPosZ = spawnPosZ;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit x = GameObject.Instantiate(replayExecutor.replayManager.prefabs[unitData.baseData.unitId]).GetComponent<ReplayUnit>();

        InitProfile(replayExecutor);
        InitUnit(replayExecutor, x);
    }

    public void InitUnit(ReplayExecutor executor, ReplayUnit unit)
    {
        unit.transform.position = new Vector3(spawnPosX, spawnPosY + .5f, spawnPosZ);
        if (side == 1)
        {
            unit.transform.rotation = Quaternion.Euler(0, -90, 0);
            unit.side = side;
        }
        executor.replayObjects.Add(unit);
        executor.replayUnits.Add(unit);
        unit.globalId = globalSpawnId;
        unit.unitData = unitData;
        unit.currentHealth = unitData.unitHealth;
    }

    public void InitProfile(ReplayExecutor executor)
    {
        ReplayProfile y = GameObject.Instantiate(executor.replayManager.profile).GetComponent<ReplayProfile>();
        executor.profiles.Add(y);
        y.globalId = globalSpawnId;
        y.SetName(unitData.baseData.unitName);
        y.SetImage(unitData.baseData.unitSprite);
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
