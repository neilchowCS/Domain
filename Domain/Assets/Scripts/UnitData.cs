using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitData
{
    public UnitDataScriptableObject baseData;

    public int unitHealth { get { return baseData.baseHealth; } }
    public int unitAttack { get { return baseData.baseAttack; } }
    public int unitDefense { get { return baseData.baseDefense; } }
    public int unitMDefense { get { return baseData.baseMDefense; } }
    public float unitAttackSpeed { get { return baseData.baseAttackSpeed; } }
    public float unitRange { get { return baseData.baseRange; } }
    public float unitMoveSpeed { get { return baseData.baseMoveSpeed; } }
    public int unitMana { get { return baseData.baseMana; } }
    public int unitTickPerMana { get { return baseData.baseTickPerMana; } }
    public float unitCrit { get { return baseData.baseCrit; } }
    public float unitCritChance { get { return baseData.baseCritChance; } }

    public UnitData(UnitDataScriptableObject scriptableObject)
    {
        baseData = scriptableObject;
    }
}