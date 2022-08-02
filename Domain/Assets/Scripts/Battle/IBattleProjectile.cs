using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleProjectile : IBattleObject
{
    public IBattleUnit Source { get; set; }

    public int SourceGlobalId { get; set; }

    public FixedUnitState UnitState { get; set; }

    public UnitAttackDataScriptableObject AttackData { get; set; }

    public BattleProjectileActions Actions { get; set; }

    public Vector3 Position { get; set; }

    public IBattleUnit TargetUnit { get; set; }
    public Vector3 TargetLocation { get; set; }
}
