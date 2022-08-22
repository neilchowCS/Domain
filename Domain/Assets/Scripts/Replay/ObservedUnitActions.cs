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

    public override void NewProjectile(IBattleUnit source,
        int index, IBattleUnit target)
    {
        unit.Executor.factory.NewObservedProjectile(source, index, target);
    }

    public override void NewProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        unit.Executor.factory.NewObservedProjectile(source, index, target);
    }
}
