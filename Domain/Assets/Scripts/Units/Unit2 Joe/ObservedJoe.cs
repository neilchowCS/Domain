using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;

public class ObservedJoe : ObservedUnit
{
    public override void SkillProjectileEffect()
    {
        Executor.mapTilesObj[CurrentTarget.X][CurrentTarget.Y].SetRed();
        List<(int, int)> neighbors = Executor.hexagonFunctions.GetNeighbors(CurrentTarget.X, CurrentTarget.Y);
        foreach ((int,int) i in neighbors)
        {
            Executor.mapTilesObj[i.Item1][i.Item2].SetRed();
        }

        List<IBattleUnit> targets = new();
        targets.Add(CurrentTarget);
        foreach ((int, int) tile in neighbors)
        {
            if (Executor.hexMap[tile.Item1, tile.Item2].occupant != null &&
                Executor.hexMap[tile.Item1, tile.Item2].occupant.Side != this.Side)
            {
                targets.Add(Executor.hexMap[tile.Item1, tile.Item2].occupant);
            }
        }

        foreach (IBattleUnit target in targets)
        {
            ActionExtension.ActionExtension.DealDamage(this, new() { target },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.special);
        }
    }
}
