using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusBurn : StatusFramework
{
    public int dmgPerTick;

    public StatusBurn(BattleExecutor exec, int side, IBattleObject source,
        IBattleUnit host, int duration, int dmgPerTick) :
        base(exec, side, "StatusBurn", source, host, StatusType.debuff, duration)
    {
        this.dmgPerTick = dmgPerTick;
        InflictBurn();
    }

    public override void OnEndTurn()
    {
        if (Executor.actingUnit == Host)
        {
            InflictBurn();
            base.OnEndTurn();
        }
    }

    public override void OnUnapply()
    {
        base.OnUnapply();
    }

    public virtual void InflictBurn()
    {
        Executor.eventManager.InitiateTriggers(ActionExtension.ActionExtension.ProcessDamage(Source, new() { Host },
           dmgPerTick, DamageType.special, AbilityType.Dot));
    }
}
