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
        status.Executor.GetAlliedObjects(status).Remove(status);
        status.Host.StatusList.Remove(status);
    }
}
