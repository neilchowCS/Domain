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
        Debug.Log(executor.globalTick);

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
        //Alice (x) dealt x damage to Alice(y)
        if (damageSource != null)
        {
            Debug.Log(damageSource.objectName + " (" + damageSource.globalObjectId + ")"
            + " dealt " + amount + " damage to "
            + damageTarget.objectName + " (" + damageTarget.globalObjectId + ")");
        }
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
        //Alice (x) took x damage from Alice(y)
        Debug.Log(damageTarget.objectName + " (" + damageTarget.globalObjectId + ")"
            + " took " + amount + " damage from "
            + damageSource.objectName + " (" + damageSource.globalObjectId + ")");
        //Alice (x) has x health remaining out of y
        Debug.Log(damageTarget.objectName + " (" + damageTarget.globalObjectId + ")"
            + " has " + damageTarget.unitHealth + " health remaining out of "
            + damageTarget.unitData.unitHealth);
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
        //Alice (x) died
        Debug.Log(deadUnit.objectName + " (" + deadUnit.globalObjectId + ")"
            + " died");
        UnitDeathEventHandler handler = UnitDeath;
        handler?.Invoke(deadUnit);
    }
}
