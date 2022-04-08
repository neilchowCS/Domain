using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitDataSO", order = 1)]
public class UnitDataScriptableObject : ScriptableObject
{
    public int unitId;
    public string unitName;
    public Sprite unitSprite;

    public int baseHealth;
    public int baseAttack;
    public int baseDefense;
    public int baseMDefense;
    public float baseAttackSpeed;
    public float baseRange;
    public float baseMoveSpeed;
    public int baseMana;
    public int baseTickPerMana;
    public float baseCrit;
    public float baseCritChance;

    public bool[] eventSubscriptions;
}