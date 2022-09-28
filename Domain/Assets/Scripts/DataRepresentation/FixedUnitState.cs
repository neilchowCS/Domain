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
    public readonly float speed;
    public readonly int maxMana;
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
        speed = unit.UnitData.unitSpeed.Value;
        maxMana = unit.UnitData.unitMaxMana.Value;
        crit = unit.UnitData.unitCrit.Value;
        critChance = unit.UnitData.unitCritChance.Value;
    }
}
