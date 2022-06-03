using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitRuntimeData
{
    public UnitDataScriptableObject baseData;
    public UnitIndividualData individualData;

    private float levelMultiplier;

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
    public AttributeFloat unitMoveSpeed;
    public AttributeInt unitMaxMana;
    public AttributeInt unitTickPerMana;
    public AttributeFloat unitCrit;
    public AttributeFloat unitCritChance;

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
        unitMoveSpeed = new AttributeFloat(baseData.baseMoveSpeed);
        unitMaxMana = new AttributeInt(baseData.baseMaxMana);
        unitTickPerMana = new AttributeInt(baseData.baseTickPerMana);
        unitCrit = new AttributeFloat(baseData.baseCrit);
        unitCritChance = new AttributeFloat(baseData.baseCritChance);

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