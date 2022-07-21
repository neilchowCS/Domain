using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeBehavior : UnitBehavior
{
    public DoeBehavior(IBattleUnit host) : base(host)
    {

    }

    public override void SpawnProjectile(int i)
    {
        if (unit.CurrentTarget != null)
        {
            BattleProjectile x = null;
            switch (i)
            {
                case 0:
                    x = new BattleProjectile(unit.Executor, unit.Side, unit, i, unit.CurrentTarget);
                    break;
                case 1:
                    x = new DoeSkillBP(unit.Executor, unit.Side, unit, i, unit.Position);
                    break;
            }
        }
    }
}
