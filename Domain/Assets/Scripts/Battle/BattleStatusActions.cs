using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unobserved specific, superclass
/// </summary>
public class BattleStatusActions
{
    public readonly IBattleStatus status;

    public BattleStatusActions(IBattleStatus host)
    {
        status = host;
    }

    public virtual void RemoveFromHost()
    {
        //status.Executor.eventHandler.TickUp -= status.Behavior.OnTickUp;
        //status.Executor.eventHandler.UnitDeath -= status.Behavior.OnUnitDeath;
        status.Executor.GetAlliedObjects(status).Remove(status);
        status.Host.StatusList.Remove(status);
    }
}
