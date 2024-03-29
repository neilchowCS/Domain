using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedStatusBurn : ObservedStatusFramework
{
    public int dmgPerTick;

    public virtual void Initialize(BattleExecutor executor, IBattleObject source,
        IBattleUnit host, int duration, int dmgPerTick)
    {
        Initialize(executor, "burn status", source, host, StatusType.debuff, duration);
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
        foreach (ParticleSystem pSys in particleSystems)
        {
            pSys.Play();
        }
    }
}
