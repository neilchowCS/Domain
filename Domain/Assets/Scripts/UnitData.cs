using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitData
{
    public int id;
    public string baseName;
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

    public void SetValues(string name, int health, int attack, int defense, int mDefense, float attackSpeed,
        float range, float moveSpeed, int mana, int tickPerMana, float crit, float critChance)
    {
        baseName = name;
        baseHealth = health;
        baseAttack = attack;
        baseDefense = defense;
        baseMDefense = mDefense;
        baseAttackSpeed = attackSpeed;
        baseRange = range;
        baseMoveSpeed = moveSpeed;
        baseMana = mana;
        baseTickPerMana = tickPerMana;
        baseCrit = crit;
        baseCritChance = critChance;
    }

    public UnitData(int i)
    {
        this.id = i;
        switch (i)
        {
            case 0:
                SetValues("Alice", 10, 1, 0, 0, 1, 1.75f * 3, .75f, 10, 20, 2f, .1f);
                break;
            case 1:
                SetValues("Bob", 15, 1, 0, 0, 1, 1.75f * 2, .75f, 10, 20, 2f, .1f);
                break;
        }
    }

    public BattleUnit GetBattleUnit(BattleExecutor exec, int side)
    {
        BattleUnit output = null;
        switch (id)
        {
            case 0:
                output = new AliceBU(exec, side, this);
                break;
            case 1:
                output = new BobBU(exec, side, this);
                break;
        }
        return output;
    }
}