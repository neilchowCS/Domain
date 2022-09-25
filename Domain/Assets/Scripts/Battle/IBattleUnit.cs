using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleBehaviorExtension;

public interface IBattleUnit : IBattleObject
{
    public UnitRuntimeData UnitData { get; set; }
    public BattleUnitActions Actions { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public int CurrentTile { get; set; }
    public int TargetTile { get; set; }

    public IBattleUnit CurrentTarget { get; set; }

    public bool NeedsCleaning { get; set; }
    public bool IsMoving { get; set; }

    public AttackStates AttackState { get; set; }
    public float TickOfLastAttack { get; set; }
    public float AttackTimer { get; set; }

    public int ManaCounter { get; set; }

    public List<IBattleStatus> StatusList { get; set; }
}
