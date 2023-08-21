using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;
using System.Linq;

public class BattleJoe : BattleUnit
{
    public BattleJoe(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileX, int tileY) : base(exec, side, unitData, tileX, tileY)
    {

    }

    public override void PerformSkill()
    {
        List<(int, int)> neighbors = Executor.hexagonFunctions.GetNeighbors(CurrentTarget.X, CurrentTarget.Y);
        List<IBattleUnit> targets = MovementExtension.GetEnemiesInTiles(this, neighbors);
        targets.Add(CurrentTarget);

        Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(this, targets,
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.normal, AbilityType.Skill).Cast<IEventTrigger>().ToList());

        foreach (IBattleUnit target in targets)
        {
            ActionExtension.ActionExtension.NewBurnStatus(this, target, 2, UnitData.unitAttack.Value);
        }
    }
}
