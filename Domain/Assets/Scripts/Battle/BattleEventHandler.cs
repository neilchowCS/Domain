using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventHandler
{
    public BattleExecutor executor;

    public BattleEventHandler(BattleExecutor exec)
    {
        executor = exec;
    }

    public delegate void TickUpEventHandler();
    public event TickUpEventHandler TickUp;
    public virtual void OnTickUp()
    {
        Debug.Log(executor.globalTick);

        TickUpEventHandler handler = TickUp;
        handler?.Invoke();

        executor.globalTick++;
    }

    public delegate void DamageDealtEventHandler(BattleUnit damageSource, BattleUnit damageTarget, int amount);
    public event DamageDealtEventHandler DamageDealt;
    public virtual void OnDamageDealt(BattleUnit damageSource, BattleUnit damageTarget, int amount)
    {
        //Alice (x) dealt x damage to Alice(y)
        Debug.Log(damageSource.objectName + " (" + damageSource.globalObjectId + ")"
            + " dealt " + amount + " damage to "
            + damageTarget.objectName + " (" + damageTarget.globalObjectId + ")");
        DamageDealtEventHandler handler = DamageDealt;
        handler?.Invoke(damageSource, damageTarget, amount);
    }

    public delegate void DamageTakenEventHandler(BattleUnit damageTarget, BattleUnit damageSource, int amount);
    public event DamageTakenEventHandler DamageTaken;
    public virtual void OnDamageTaken(BattleUnit damageTarget, BattleUnit damageSource, int amount)
    {
        //Alice (x) took x damage from Alice(y)
        Debug.Log(damageTarget.objectName + " (" + damageTarget.globalObjectId + ")"
            + " took " + amount + " damage from "
            + damageSource.objectName + " (" + damageSource.globalObjectId + ")");
        //Alice (x) has x health remaining out of y
        Debug.Log(damageTarget.objectName + " (" + damageTarget.globalObjectId + ")"
            + " has " + damageTarget.unitHealth + " health remaining out of "
            + damageTarget.unitMaxHealth);
        DamageTakenEventHandler handler = DamageTaken;
        handler?.Invoke(damageTarget, damageSource, amount);
    }

    public delegate void UnitDeathEventHandler(BattleUnit deadUnit);
    public event UnitDeathEventHandler UnitDeath;
    public virtual void OnUnitDeath(BattleUnit deadUnit)
    {
        //Alice (x) died
        Debug.Log(deadUnit.objectName + " (" + deadUnit.globalObjectId + ")"
            + " died");
        UnitDeathEventHandler handler = UnitDeath;
        handler?.Invoke(deadUnit);
    }
}
