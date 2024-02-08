using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;

public interface IBattleUnit : IBattleObject
{
    public UnitRuntimeData UnitData { get; set; }
    public AttributeInt UnitSpeed { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public bool IsDead { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public IBattleUnit CurrentTarget { get; set; }

    public List<IBattleStatus> StatusList { get; set; }

    public void HandleDeath(IBattleUnit deadUnit);

    //Behavior
    public void PerformAction();

    public void ModifyHealth(int amount, DamageType damageType);
    public void ModifyMana(int amount);


}