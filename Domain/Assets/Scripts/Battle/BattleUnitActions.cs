using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitActions
{
    protected BattleUnit unit;

    public BattleUnitActions(BattleUnit unit)
    {
        this.unit = unit;
    }

    public virtual void SetPosition(Vector3 position)
    {
        unit.position = position;
    }
}