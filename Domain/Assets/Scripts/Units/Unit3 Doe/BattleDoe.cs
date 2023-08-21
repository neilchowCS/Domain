using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;
using System.Linq;

public class BattleDoe : BattleUnit
{
    public BattleDoe(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileX, int tileY) : base(exec, side, unitData, tileX, tileY)
    {

    }

    public override void PerformSkill()
    {
        List<(int, int)> line = Executor.hexagonFunctions.GetLine(X,Y,CurrentTarget.X,CurrentTarget.Y,3);
        List<IBattleUnit> targets = MovementExtension.GetEnemiesInTiles(this, line);

        Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(this, targets,
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.normal, AbilityType.Skill).Cast<IEventTrigger>().ToList()
        );
    }
}
