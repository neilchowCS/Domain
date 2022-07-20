using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitActions
{
    protected IBattleUnit unit;

    public BattleUnitActions(IBattleUnit unit)
    {
        this.unit = unit;
    }

    public virtual void SetPosition(Vector3 position)
    {
        unit.Position = position;
    }
}