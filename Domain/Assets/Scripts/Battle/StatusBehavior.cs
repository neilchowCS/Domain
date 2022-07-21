using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBehavior : ObjectBehavior
{
    protected readonly IBattleStatus status;

    public StatusBehavior(IBattleStatus host)
    {
        status = host;
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == status.Host)
        {
            OnUnapply();
            status.Host = null;
        }
    }

    public override void OnSpawn()
    {
        
    }

    public virtual void OnUnapply()
    {
        if (status.Host != null)
        {
            status.Actions.RemoveFromHost();
        }
    }
}
