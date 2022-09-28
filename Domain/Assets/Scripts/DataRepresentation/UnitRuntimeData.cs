using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UnitRuntimeData
{
    public UnitDataScriptableObject baseData;
    public UnitIndividualData individualData;

    private float levelMultiplier;

    [Header("Attributes")]
    public int health;
    public int mana;
    public AttributeInt unitMaxHealth;
    public AttributeInt unitAttack;
    public AttributeInt unitDefense;
    public AttributeInt unitMDefense;
    public float armorReduction { get; private set; }
    public AttributeFloat unitAttackSpeed;
    public float ticksPerAttack { get; private set; }
    public AttributeFloat unitRange;
    public AttributeInt unitSpeed;
    public AttributeInt unitMaxMana;
    public AttributeFloat unitCrit;
    public AttributeFloat unitCritChance;

    public AttributeFloat unitRecovery;

    public UnitRuntimeData((UnitDataScriptableObject, UnitIndividualData) compositeData)
    {
        this.baseData = compositeData.Item1;
        this.individualData = compositeData.Item2;
        levelMultiplier = 1 + ((individualData.level - 1) * 0.085f);

        health = (int)(baseData.baseHealth * levelMultiplier);
        mana = baseData.baseStartingMana;
        unitMaxHealth = new AttributeInt(health);
        unitAttack = new AttributeInt((int)(baseData.baseAttack * levelMultiplier));
        unitDefense = new AttributeInt((int)(baseData.baseDefense * levelMultiplier));
        unitMDefense = new AttributeInt((int)(baseData.baseMDefense * levelMultiplier));
        unitAttackSpeed = new AttributeFloat(baseData.baseAttackSpeed);
        unitRange = new AttributeFloat(baseData.baseRange);
        unitSpeed = new AttributeInt((int)(baseData.baseSpeed * levelMultiplier));
        unitMaxMana = new AttributeInt(baseData.baseMaxMana);
        unitCrit = new AttributeFloat(baseData.baseCrit);
        unitCritChance = new AttributeFloat(baseData.baseCritChance);

        unitRecovery = new AttributeFloat(baseData.baseMovementRecovery);

        ticksPerAttack = TickSpeed.ticksPerSecond / unitAttackSpeed.Value;
        armorReduction = Mathf.Pow(0.25f, unitDefense.Value / (levelMultiplier * 100));
    }

    public void ModifyAttackSpeed(float modifier)
    {
        unitAttackSpeed.ModifyMultiplicative(modifier);
        ticksPerAttack = TickSpeed.ticksPerSecond / unitAttackSpeed.Value;
    }

    //Hardcode coefficients
    public void ModifyArmor(float modifier)
    {
        unitDefense.ModifyMultiplicative(modifier);
        armorReduction = Mathf.Pow(0.25f, unitDefense.Value / (levelMultiplier * 100));
    }
}