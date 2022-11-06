using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorExtension;

public interface IBattleUnit : IBattleObject
{
    public UnitRuntimeData UnitData { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public int Tile { get; set; }

    public IBattleUnit CurrentTarget { get; set; }

    public List<IBattleStatus> StatusList { get; set; }

    //Behavior
    public void PerformAction();

    public void ModifyHealth(int amount, DamageType damageType, IBattleUnit source);
    public void ModifyMana(int amount);
    public void SelfDeath();
}