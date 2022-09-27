using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoeTargetingExtension;

public class JoeBehavior : UnitBehavior
{
    public JoeBehavior(IBattleUnit host) : base(host)
    {

    }

    public override void SpawnProjectile(int i)
    {
        if (unit.CurrentTarget != null)
        {
            switch (i)
            {
                case 0:
                    //unit.Executor.factory.NewProjectile(unit, i, unit.CurrentTarget);
                    //x = new BattleProjectile(unit.Executor, unit.Side, unit, i, unit.CurrentTarget);
                    break;
                case 1:
                    //unit.Executor.factory.NewProjectile(unit, i, unit.GetAoeLocation(3, 0));
                    //x = new JoeSkillBP(unit.Executor, unit.Side, unit, i, unit.GetAoeLocation(3, 0));
                    break;
            }
        }
    }
}
