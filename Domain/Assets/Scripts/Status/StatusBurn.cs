using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusBurn : StatusFramework
{
    public int dmgPerTick;

    public StatusBurn(BattleExecutor exec, int side, IBattleObject source,
        IBattleUnit host, int duration, int dmgPerTick):
        base(exec, side, "StatusBurn", source, host, StatusType.debuff, duration)
    {
        this.dmgPerTick = dmgPerTick;
    }

    public override void OnEndTurn()
    {
        InflictBurn();
        base.OnEndTurn();
    }

    public override void OnSpawn(IBattleObject source)
    {
        if (source == this)
        {
            InflictBurn();
        }
    }

    public override void OnUnapply()
    {
        base.OnUnapply();
    }

    public virtual void InflictBurn()
    {
        Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(Source, new() { Host },
           dmgPerTick, DamageType.special, AbilityType.Dot).Cast<IEventCommand>().ToList()
        );
    }
}
