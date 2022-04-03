using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawn : TimelineEvent
{
    public int spawnId;

    public EventSpawn(int spawnId)
    {
        this.spawnId = spawnId;
    }

    public override int[] GetData()
    {
        Debug.Log("Spawn Event (spawnId)");
        return new int[] { spawnId };
    }
}
