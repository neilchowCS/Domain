using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EventSubscriber
{
    public static void Subscribe(BattleUnit unit, bool[] list)
    {
        if (list[0])
        {
            unit.Executor.eventHandler.DamageDealt += unit.OnDamageDealt;
        }
        if (list[1])
        {
            unit.Executor.eventHandler.DamageTaken += unit.OnDamageTaken;
        }
        if (list[2])
        {
            unit.Executor.eventHandler.UnitDeath += unit.OnUnitDeath;
        }
        if (list[3])
        {
            unit.Executor.eventHandler.HealApplied += unit.OnHealApplied;
        }
        if (list[4])
        {
            //unit.executor.eventHandler.UnitDeath += unit.OnUnitTarget;
        }
    }
}