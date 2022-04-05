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
    public int spawnId;
    public int globalSpawnId;
    public int side;
    public int spawnTileId;
    public float spawnPosX;
    public float spawnPosY;
    public float spawnPosZ;
    public int maxHp;

    /// <summary>
    /// 0 = spawnId
    /// 1 = globalSpawnId
    /// 2 = side
    /// 3 = spawnTileId
    /// 4 = spawnPosX
    /// 5 = spawnPosY
    /// 6 = spawnPosZ
    /// </summary>
    public TimelineSpawn(int spawnId,
        int globalSpawnId,
        int side,
        int spawnTileId,
        float spawnPosX,
        float spawnPosY,
        float spawnPosZ,
        int maxHp)
    {
        this.spawnId = spawnId;
        this.globalSpawnId = globalSpawnId;
        this.side = side;
        this.spawnTileId = spawnTileId;
        this.spawnPosX = spawnPosX;
        this.spawnPosY = spawnPosY;
        this.spawnPosZ = spawnPosZ;
        this.maxHp = maxHp;
    }

    public override float[] GetData()
    {
        //Debug.Log("Spawn");
        return new float[] { spawnId, globalSpawnId, spawnTileId, side,
        spawnPosX, spawnPosY, spawnPosZ, maxHp };
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        GameObject x = GameObject.Instantiate(replayExecutor.rm.prefabs[spawnId]);
        x.transform.position = new Vector3(spawnPosX, spawnPosY + .5f, spawnPosZ);
        if (side == 1)
        {
            x.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        replayExecutor.replayObjects.Add(x.GetComponent<ReplayUnit>());
        replayExecutor.replayUnits.Add(x.GetComponent<ReplayUnit>());
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].globalId = globalSpawnId;
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].maxHealth = maxHp;
        replayExecutor.replayUnits[replayExecutor.replayUnits.Count - 1].currentHealth = maxHp;
    }
}
