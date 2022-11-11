using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;

public class BattleJoe : BattleUnit
{
    public BattleJoe(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileX, int tileY) : base(exec, side, unitData, tileX, tileY)
    {

    }

    public override void PerformSkill()
    {
        List<IBattleUnit> targets = new();
        targets.Add(CurrentTarget);
        List<(int, int)> neighbors = Executor.hexagonFunctions.GetNeighbors(CurrentTarget.X, CurrentTarget.Y);
        foreach (IBattleUnit enemy in Executor.GetEnemyUnits(this))
        {
            if (neighbors.Contains((enemy.X, enemy.Y)))
            {
                targets.Add(enemy);
            }
        }

        foreach(IBattleUnit target in targets)
        {
            ActionExtension.ActionExtension.DealDamage(this, new() { target },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.special);
        }
    }
}
