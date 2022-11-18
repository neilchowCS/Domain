using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;
using System.Linq;

public class ObservedDoe : ObservedUnit
{
    public override void SkillProjectileEffect()
    {
        List<(int, int)> line = Executor.hexagonFunctions.GetLine(X, Y, CurrentTarget.X, CurrentTarget.Y, 3);
        foreach ((int, int) i in line)
        {
            Executor.mapTilesObj[i.Item1][i.Item2].SetRed();
        }

        List<IBattleUnit> targets = MovementExtension.GetEnemiesInTiles(this, line);

        Executor.EnqueueEvent(ActionExtension.ActionExtension.ProcessDamage(this, targets,
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.normal, AbilityType.Skill).Cast<IEventCommand>().ToList()
        );
    }
}
