using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : BattleObject
{
    public IBattleUnit host;

    public BattleStatus(BattleExecutor exec, int side, string name, IBattleUnit host)
        :base(exec, side, name){

        this.host = host;
        Executor.eventHandler.UnitDeath += this.OnUnitDeath;
    }

    public virtual void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == host)
        {
            OnUnapply();
            this.host = null;
        }
    }

    public virtual void OnUnapply()
    {
        if (host != null)
        {
            host.StatusList.Remove(this);
        }
    }
}
