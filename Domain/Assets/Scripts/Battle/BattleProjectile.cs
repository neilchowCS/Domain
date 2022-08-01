using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProjectile : BattleObject, IBattleProjectile
{
    public BattleProjectileActions Actions { get; }

    public IBattleUnit Source { get; set; }
    public int SourceGlobalId { get; set; } = -1;

    public FixedUnitState UnitState { get; set; }

    public Vector3 Position { get; set; }

    public IBattleUnit TargetUnit { get; set; }
    public Vector3 TargetLocation { get; set; }
    public UnitAttackDataScriptableObject AttackData { get; set; }

    public BattleProjectile(BattleExecutor exec, int side, IBattleUnit source,
        int index, IBattleUnit target) : base(exec, side, "Default Projectile")
    {
        Executor.eventHandler.UnitDeath += Behavior.OnUnitDeath;
        this.Source = source;
        this.Position = source.Position;
        AttackData = source.UnitData.baseData.attackDataList[index];
        this.TargetUnit = target;
        UnitState = new FixedUnitState(source);
        SourceGlobalId = source.GlobalObjectId;
    }

    public BattleProjectile(BattleExecutor exec, int side, IBattleUnit source,
        int index, Vector3 targetLocation) : base(exec, side, "Default Projectile")
    {
        this.Source = source;
        this.Position = source.Position;
        AttackData = source.UnitData.baseData.attackDataList[index];
        this.TargetLocation = targetLocation;
        UnitState = new FixedUnitState(source);
        SourceGlobalId = source.GlobalObjectId;
    }
}