using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUnit : IBattleObject
{
    public UnitRuntimeData UnitData { get; set; }
    public BattleUnitActions Actions { get; set; }

    public Vector3 Position { get; set; }

    public BattleTile CurrentTile { get; set; }
    public BattleTile TargetTile { get; set; }

    public IBattleUnit CurrentTarget { get; set; }

    public bool NeedsCleaning { get; set; }
    public bool IsMoving { get; set; }

    public AttackStates AttackState { get; set; }
    public float TickOfLastAttack { get; set; }
    public float AttackTimer { get; set; }

    public int ManaCounter { get; set; }

    public List<BattleStatus> StatusList { get; set; }


}
