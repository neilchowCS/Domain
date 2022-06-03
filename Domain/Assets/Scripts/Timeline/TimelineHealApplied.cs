using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHealApplied : TimelineEvent
{
    public int sourceId;
    public int targetId;
    public int amount;

    public TimelineHealApplied(int sourceId, int targetId, int amount)
    {
        this.sourceId = sourceId;
        this.targetId = targetId;
        this.amount = amount;
    }

    public override void ExecuteEvent(ReplayExecutor replayExecutor)
    {
        ReplayUnit source = null;
        ReplayUnit target = null;
        foreach (ReplayUnit rO in replayExecutor.replayUnits)
        {
            if (rO.globalId == sourceId)
            {
                source = rO;
            }
            if (rO.globalId == targetId)
            {
                target = rO;
            }
        }
        if (target != null)
        {
            target.unitData.health += amount;
        }
        else
        {
            Debug.Log("DamageFailed");
            return;
        }

        /*
        foreach (ReplayProfile i in replayExecutor.profiles)
        {
            if (i.globalId == sourceId)
            {
                i.IncreaseDamage(amount);
                break;
            }
        }
        */

        replayExecutor.CreateDamageNumber(target.transform.position, amount, DamageType.healing);
        //replayExecutor.ReorderProfile();
    }
}
