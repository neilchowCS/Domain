using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitRuntimeData
{
    public UnitDataScriptableObject baseData;
    public UnitIndividualData individualData;

    public int health;
    public int mana;
    public AttributeInt unitMaxHealth;
    public AttributeInt unitAttack;
    public AttributeInt unitDefense;
    public AttributeInt unitMDefense;
    public AttributeFloat unitAttackSpeed;
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

        health = baseData.baseHealth;
        mana = 0;
        unitMaxHealth = new AttributeInt(baseData.baseHealth);
        unitAttack = new AttributeInt(baseData.baseAttack);
        unitDefense = new AttributeInt(baseData.baseDefense);
        unitMDefense = new AttributeInt(baseData.baseMDefense);
        unitAttackSpeed = new AttributeFloat(baseData.baseAttackSpeed);
        unitRange = new AttributeFloat(baseData.baseRange);
        unitMoveSpeed = new AttributeFloat(baseData.baseMoveSpeed);
        unitMaxMana = new AttributeInt(baseData.baseMana);
        unitTickPerMana = new AttributeInt(baseData.baseTickPerMana);
        unitCrit = new AttributeFloat(baseData.baseCrit);
        unitCritChance = new AttributeFloat(baseData.baseCritChance);
    }
}