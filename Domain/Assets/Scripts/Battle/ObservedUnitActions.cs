using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedUnitActions : BattleUnitActions
{
    public ObservedUnitActions(IBattleUnit unit): base(unit)
    {

    }

    public override void SetPosition(Vector3 position)
    {
        unit.Position = position;
    }
}
