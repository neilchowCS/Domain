using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorExtension;

public class ObservedJoe : ObservedUnit
{
    public override void SkillProjectileEffect()
    {
        Executor.mapTilesObj[CurrentTarget.Tile].SetRed();
        foreach (int i in Executor.mapGraph[CurrentTarget.Tile].connections)
        {
            Executor.mapTilesObj[i].SetRed();
        }

        List<IBattleUnit> targets = new();
        targets.Add(CurrentTarget);
        foreach (IBattleUnit enemy in Executor.GetEnemyUnits(this))
        {
            if (Executor.mapGraph[CurrentTarget.Tile].connections.Contains(enemy.Tile))
            {
                targets.Add(enemy);
            }
        }

        foreach (IBattleUnit target in targets)
        {
            ActionExtension.DealDamage(this, new() { target },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.special);
        }
    }
}
