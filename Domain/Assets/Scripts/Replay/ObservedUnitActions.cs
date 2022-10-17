using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedUnitActions : BattleUnitActions
{
    private readonly ObservedUnit unit;

    public ObservedUnitActions(ObservedUnit unit) : base(unit)
    {
        this.unit = unit;
    }

    public override void StartMoving(Vector3 moveLocation)
    {
        float time = .7f;
        unit.movementController.StartMovement(moveLocation,
            Vector3.Distance(moveLocation, unit.transform.position) / time);
    }

    public override void SetMana(int amount)
    {
        base.SetMana(amount);
        unit.healthBar.RefreshFill();
    }

    public override void TakeDamage(IBattleUnit damageSource, int amount, DamageType damageType)
    {
        base.TakeDamage(damageSource, amount, damageType);
        unit.healthBar.RefreshFill();
        unit.Executor.CreateDamageNumber(unit.Position, amount, damageType);
    }

    public override void DealtDamage(int amount)
    {
        ((ObservedBattleExecutor)unit.Executor).UpdateProfileDamage(unit.GlobalObjectId, amount);

    }

    public override void SelfDeath()
    {
        unit.gameObject.SetActive(false);
        GameObject.Destroy(unit.healthBar.gameObject);
        //GameObject.Destroy(unit.GetGameObject());
    }

    public override void NewProjectile(int index)
    {
        unit.Executor.factory.GetObservedProjectile(
            unit.UnitData.baseData.attackDataList[index].projectilePrefab,
            unit.Position, unit.CurrentTarget,
            unit.UnitData.baseData.attackDataList[index].speed);
    }

    public override void NewProjectile(IBattleUnit source,
        int index, Vector3 target)
    {
        //unit.Executor.factory.NewObservedProjectile(source, index, target);
    }
}
