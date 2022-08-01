using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUnitState
{
    public readonly int maxHealth;
    public readonly int attack;
    public readonly int defense;
    public readonly int mDefense;
    public readonly float attackSpeed;
    public readonly float range;
    public readonly float moveSpeed;
    public readonly int maxMana;
    public readonly int tickPerMana;
    public readonly float crit;
    public readonly float critChance;

    public FixedUnitState(IBattleUnit unit)
    {
        maxHealth = unit.UnitData.unitMaxHealth.Value;
        attack = unit.UnitData.unitAttack.Value;
        defense = unit.UnitData.unitDefense.Value;
        mDefense = unit.UnitData.unitMDefense.Value;
        attackSpeed = unit.UnitData.unitAttackSpeed.Value;
        range = unit.UnitData.unitRange.Value;
        moveSpeed = unit.UnitData.unitMoveSpeed.Value;
        maxMana = unit.UnitData.unitMaxMana.Value;
        tickPerMana = unit.UnitData.unitTickPerMana.Value;
        crit = unit.UnitData.unitCrit.Value;
        critChance = unit.UnitData.unitCritChance.Value;
    }
}
