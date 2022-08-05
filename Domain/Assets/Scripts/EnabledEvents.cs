using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnabledEvents
{
    public bool OnTick;
    public bool OnDamageDealt;
    public bool OnDamageTaken;
    public bool OnUnitDeath;
    public bool OnHealApplied;

    public EnabledEvents(bool onTick, bool onDamageDealt, bool onDamageTaken,
        bool onUnitDeath, bool onHealApplied)
    {
        OnTick = onTick;
        OnDamageDealt = onDamageDealt;
        OnDamageTaken = onDamageTaken;
        OnUnitDeath = onUnitDeath;
        OnHealApplied = onHealApplied;
    }
}
