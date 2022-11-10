using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;

public class BattleJoe : BattleUnit
{
    public BattleJoe(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId) : base(exec, side, unitData, tileId)
    {

    }

    public override void PerformSkill()
    {
        List<IBattleUnit> targets = new();
        targets.Add(CurrentTarget);
        foreach (IBattleUnit enemy in Executor.GetEnemyUnits(this))
        {
            if (Executor.mapGraph[CurrentTarget.Tile].connections.Contains(enemy.Tile))
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
