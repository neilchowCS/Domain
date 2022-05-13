using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineProjectile : TimelineEvent
{
    public int sourceId;
    public bool targeted;
    public int targetId;
    public Vector3 targetLocation;
    public int projectileIndex;

    public TimelineProjectile(int sourceId, int targetId, int projectileIndex)
    {
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.projectileIndex = projectileIndex;
        targeted = true;
    }

    public TimelineProjectile(int sourceId, Vector3 targetLocation, int projectileIndex)
    {
        this.sourceId = sourceId;
        this.targetLocation = targetLocation;
        this.projectileIndex = projectileIndex;
        targeted = false;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit source = null;
        ReplayUnit target = null;

        if (targeted)
        {
            foreach (ReplayUnit rU in replayExecutor.replayUnits)
            {
                if (rU.globalId == sourceId)
                {
                    source = rU;
                }
                if (rU.globalId == targetId)
                {
                    target = rU;
                }
            }
        }
        else
        {
            foreach (ReplayUnit rU in replayExecutor.replayUnits)
            {
                if (rU.globalId == sourceId)
                {
                    source = rU;
                }
            }
        }
        Debug.Log("Source:" + sourceId);
        Debug.Log("Count:" + source.unitData.baseData.attackDataList.Count);
        Debug.Log("Index:" + projectileIndex);
        ReplayProjectile x =
            GameObject.Instantiate(source.unitData.baseData.attackDataList[projectileIndex].projectile);

        InitProjectile(replayExecutor, x, source, target);
    }

    public void InitProjectile(ReplayExecutor executor, ReplayProjectile self,
        ReplayUnit source, ReplayUnit target)
    {
        self.transform.position = source.transform.position + new Vector3(0, 1, 0);
        self.speed = source.unitData.baseData.attackDataList[projectileIndex].speed;
        if (targeted)
        {
            if (target != null)
            {
                self.target = target;
            }
        }
        else
        {
            self.targetLocation = targetLocation;
        }
        /*
        if (side == 1)
        {
            unit.transform.rotation = Quaternion.Euler(0, -90, 0);
            unit.side = side;
        }
        */
    }

}
