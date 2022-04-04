using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSpawn : TimelineEvent
{
    public int spawnId;
    public int globalSpawnId;
    public int side;
    public int spawnTileId;
    public float spawnPosX;
    public float spawnPosY;
    public float spawnPosZ;

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
