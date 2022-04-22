using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventHandler
{
    public BattleExecutor executor;

    /// <summary>
    /// Constructor for BattleEventHandler.
    /// Called by executor on init.
    /// Holds delegates and events.
    /// </summary>
    public BattleEventHandler(BattleExecutor exec)
    {
        executor = exec;
    }

    public delegate void TickUpEventHandler();
    /// <summary>
    /// EventHandler for OnTickUp().
    /// </summary>
    public event TickUpEventHandler TickUp;
    /// <summary>
    /// Raises TickUp event. Increments global tick.
    /// </summary>
    public virtual void OnTickUp()
    {

        TickUpEventHandler handler = TickUp;
        //raise event
        handler?.Invoke();

        executor.globalTick++;
    }

    public delegate void DamageDealtEventHandler(BattleUnit damageSource, BattleUnit damageTarget, int amount);
    /// <summary>
    /// EventHandler for OnDamageDealt().
    /// </summary>
    public event DamageDealtEventHandler DamageDealt;
    /// <summary>
    /// Raises DamageDealt event.
    /// </summary>
    /// <param name="damageSource"> Where is damage coming from </param>
    /// <param name="damageTarget"> What is taking damage </param>
    /// <param name="amount"> How much damage </param>
    public virtual void OnDamageDealt(BattleUnit damageSource, BattleUnit damageTarget, int amount)
    {
        DamageDealtEventHandler handler = DamageDealt;
        //raise event
        handler?.Invoke(damageSource, damageTarget, amount);
    }

    public delegate void DamageTakenEventHandler(BattleUnit damageTarget, BattleUnit damageSource, int amount);
    /// <summary>
    /// EventHandler for OnDamageTaken().
    /// </summary>
    public event DamageTakenEventHandler DamageTaken;
    /// <summary>
    /// Raises DamageTaken event.
    /// </summary>
    /// <param name="damageTarget"> What is taking damage </param>
    /// <param name="damageSource"> Where is damage coming from </param>
    /// <param name="amount"> How much damage </param>
    public virtual void OnDamageTaken(BattleUnit damageTarget, BattleUnit damageSource, int amount)
    {
        DamageTakenEventHandler handler = DamageTaken;
        //raise event
        handler?.Invoke(damageTarget, damageSource, amount);
    }

    public delegate void UnitDeathEventHandler(BattleUnit deadUnit);
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    public event UnitDeathEventHandler UnitDeath;
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    /// <param name="deadUnit"> What died </param>
    public virtual void OnUnitDeath(BattleUnit deadUnit)
    {
        UnitDeathEventHandler handler = UnitDeath;
        handler?.Invoke(deadUnit);
    }
}
