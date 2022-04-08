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
        GameObject x = GameObject.Instantiate(replayExecutor.rm.prefabs[unitData.baseData.unitId]);
        x.transform.position = new Vector3(spawnPosX, spawnPosY + .5f, spawnPosZ);
        if (side == 1)
        {
            x.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        replayExecutor.replayObjects.Add(x.GetComponent<ReplayUnit>());
        replayExecutor.replayUnits.Add(x.GetComponent<ReplayUnit>());
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].globalId = globalSpawnId;
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].unitData = unitData;
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].currentHealth = unitData.unitHealth;


        GameObject y = GameObject.Instantiate(replayExecutor.rm.profile);
        replayExecutor.profiles.Add(y.GetComponent<ReplayProfile>());
        replayExecutor.profiles[replayExecutor.profiles.Count - 1].globalId = globalSpawnId;
        replayExecutor.profiles[replayExecutor.profiles.Count - 1].SetName(unitData.baseData.unitName);
        y.transform.SetParent(replayExecutor.rm.profileParent.transform, false);
        if (side == 1)
        {
            y.transform.localPosition = new Vector3(-y.transform.localPosition.x,
                y.transform.localPosition.y - (replayExecutor.side1)* 75 ,
                y.transform.localPosition.z);
            replayExecutor.side1++;
        }
        else
        {
            y.transform.localPosition = new Vector3(y.transform.localPosition.x,
                y.transform.localPosition.y - (replayExecutor.side0) * 75,
                y.transform.localPosition.z);
            replayExecutor.side0++;
        }
    }
}
