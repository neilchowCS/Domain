using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedUnitActions : BattleUnitActions
{
    public ObservedUnitActions(IBattleUnit unit): base(unit)
    {

    }

    public override void SelfDeath()
    {
        unit.GetGameObject().SetActive(false);
        //GameObject.Destroy(unit.GetGameObject());
    }
}
