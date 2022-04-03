using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static class UnitData
{
    public static UnitDataS[] data;

    static UnitData()
    {
        data = new UnitDataS[] {

            //Alice
            //10 hp
            //1 atk
            //1 Defense
            //1 MDefense
            //.1 AtkSpd
            //2 Range
            //.3 MoveSpd
            //10 Mana
            //20 TickPerMana
            //2 Crit
            //.1 CritChance
            new UnitDataS("Alice",10, 1, 1, 1, .1f, 2f, .3f, 10, 20, 2f, .1f)

        };
    }

}

public struct UnitDataS
{
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

    public UnitDataS(string baseName, int health, int attack, int defense, int mDefense, float attackSpeed,
        float range, float moveSpeed, int mana, int tickPerMana, float crit, float critChance)
    {
        this.baseName = baseName;
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
}