using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : BattleObject, IBattleStatus
{
    public IBattleUnit Host { get; set; }
    public BattleStatusActions Actions { get; set; }

    public BattleStatus(BattleExecutor exec, int side, string name, IBattleUnit host)
        :base(exec, side, name){

        this.Host = host;
        Executor.eventHandler.UnitDeath += Behavior.OnUnitDeath;

        Behavior.OnSpawn();
    }
}
