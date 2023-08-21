using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeathTrigger : IEventTrigger
{
    public int Id { get; set; }

    private IBattleUnit unit;

    public UnitDeathTrigger(IBattleUnit unit)
    {
        Id = 4;
        this.unit = unit;
    }

    public void Execute(IBattleObject obj)
    {
        obj.OnUnitDeath(unit);
    }
}
