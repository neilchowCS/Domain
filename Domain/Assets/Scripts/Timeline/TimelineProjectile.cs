using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineProjectile : TimelineEvent
{
    public int sourceId;
    public int targetId;
    public int projectileIndex;
    public TimelineProjectile(int sourceId, int targetId, int projectileIndex)
    {
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.projectileIndex = projectileIndex;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit source = null;
        ReplayUnit target = null;

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

        ReplayProjectile x =
            GameObject.Instantiate(source.unitData.baseData.attackDataList[projectileIndex].projectile);

        InitProjectile(replayExecutor, x, source, target);
    }

    public void InitProjectile(ReplayExecutor executor, ReplayProjectile self,
        ReplayUnit source, ReplayUnit target)
    {
        self.transform.position = source.transform.position + new Vector3(0, 1, 0);
        self.target = target;
        self.speed = source.unitData.baseData.attackDataList[projectileIndex].speed;
        /*
        if (side == 1)
        {
            unit.transform.rotation = Quaternion.Euler(0, -90, 0);
            unit.side = side;
        }
        */
    }

}
