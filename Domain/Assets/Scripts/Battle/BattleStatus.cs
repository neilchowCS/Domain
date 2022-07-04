using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : BattleObject
{
    public BattleUnit host;

    public BattleStatus(BattleExecutor exec, int side, string name, BattleUnit host)
        :base(exec, side, name){

        this.host = host;
        executor.eventHandler.UnitDeath += this.OnUnitDeath;
    }

    public virtual void OnUnitDeath(BattleUnit deadUnit)
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
            host.statusList.Remove(this);
        }
    }
}
