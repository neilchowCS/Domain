using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObservedStatusBurn : ObservedStatusFramework
{
    public int dmgPerTick;

    public override void OnEndTurn()
    {
        if (Executor.actingUnit == Host)
        {
            InflictBurn();
            base.OnEndTurn();
        }
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
        foreach (ParticleSystem pSys in particleSystems)
        {
            pSys.Play();
        }
    }
}
