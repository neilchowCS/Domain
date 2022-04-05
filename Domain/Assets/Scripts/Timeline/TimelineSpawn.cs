using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// eventId 1
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

    /// <summary>
    /// eventId
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
        float spawnPosZ)
    {
        eventId = 1;
        this.spawnId = spawnId;
        this.globalSpawnId = globalSpawnId;
        this.side = side;
        this.spawnTileId = spawnTileId;
        this.spawnPosX = spawnPosX;
        this.spawnPosY = spawnPosY;
        this.spawnPosZ = spawnPosZ;
    }

    public override float[] GetData()
    {
        //Debug.Log("Spawn");
        return new float[] { spawnId, globalSpawnId, spawnTileId, side,
        spawnPosX, spawnPosY, spawnPosZ };
    }
}
