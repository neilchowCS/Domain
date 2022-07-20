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

    public delegate void DamageDealtEventHandler(IBattleUnit damageSource, IBattleUnit damageTarget, int amount);
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
    public virtual void OnDamageDealt(IBattleUnit damageSource, IBattleUnit damageTarget, int amount)
    {
        DamageDealtEventHandler handler = DamageDealt;
        //raise event
        handler?.Invoke(damageSource, damageTarget, amount);
    }

    public delegate void DamageTakenEventHandler(IBattleUnit damageTarget, IBattleUnit damageSource, int amount);
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
    public virtual void OnDamageTaken(IBattleUnit damageTarget, IBattleUnit damageSource, int amount)
    {
        DamageTakenEventHandler handler = DamageTaken;
        //raise event
        handler?.Invoke(damageTarget, damageSource, amount);
    }

    public delegate void UnitDeathEventHandler(IBattleUnit deadUnit);
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    public event UnitDeathEventHandler UnitDeath;
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    /// <param name="deadUnit"> What died </param>
    public virtual void OnUnitDeath(IBattleUnit deadUnit)
    {
        UnitDeathEventHandler handler = UnitDeath;
        handler?.Invoke(deadUnit);
    }

    public delegate void HealAppliedEventHandler(IBattleUnit healSource, IBattleUnit healTarget, int amount);
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    public event HealAppliedEventHandler HealApplied;
    /// <summary>
    /// Raises UnitDeath event.
    /// </summary>
    /// <param name="deadUnit"> What died </param>
    public virtual void OnHealApplied(IBattleUnit healSource, IBattleUnit healTarget, int amount)
    {
        HealAppliedEventHandler handler = HealApplied;
        handler?.Invoke(healSource, healTarget, amount);
    }
}
