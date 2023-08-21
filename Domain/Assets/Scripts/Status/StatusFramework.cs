using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusFramework : BattleObject, IBattleStatus
{
    public IBattleUnit Host { get; set; }
    public IBattleObject Source { get; set; }
    public StatusType statusType;
    public bool hasDuration;
    public int duration;

    public StatusFramework(BattleExecutor exec, int side, string name, IBattleObject source,
        IBattleUnit host, StatusType sType) : base(exec, side, name)
    {
        Host = host;
        Source = source;
        hasDuration = false;
        statusType = sType;
    }

    public StatusFramework(BattleExecutor exec, int side, string name, IBattleObject source,
        IBattleUnit host, StatusType sType, int duration):base(exec, side, name)
    {
        Host = host;
        Source = source;
        hasDuration = true;
        this.duration = duration;
        statusType = sType;
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == Host)
        {
            Executor.eventManager.RemoveObject(this);
        }
    }

    public override void OnEndTurn()
    {
        if (hasDuration && Executor.actingUnit == Host)
        {
            duration--;
            if (duration <= 0)
            {
                Executor.eventManager.RemoveObject(this);
            }
        }
    }

    public virtual void RemoveStatus()
    {
        OnUnapply();
        Host.StatusList.Remove(this);
        //Executor.eventManager.RemoveObject(this, Host);
    }

    public virtual void OnUnapply()
    {

    }
}
