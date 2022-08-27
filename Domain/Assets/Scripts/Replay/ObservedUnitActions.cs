using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedUnitActions : BattleUnitActions
{
    private readonly ObservedUnit unit;

    public ObservedUnitActions(ObservedUnit unit): base(unit)
    {
        this.unit = unit;
    }

    public override void SetMana(int amount)
    {
        base.SetMana(amount);
        unit.healthBar.manaFill.fillAmount = unit.UnitData.mana / (float)unit.UnitData.unitMaxMana.Value;
    }

    public override void TakeDamage(IBattleUnit damageSource, int amount)
    {
        base.TakeDamage(damageSource, amount);
        unit.healthBar.healthFill.fillAmount = unit.UnitData.health / (float)unit.UnitData.unitMaxHealth.Value;
    }

    public override void SelfDeath()
    {
        unit.gameObject.SetActive(false);
        GameObject.Destroy(unit.healthBar.gameObject);
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
