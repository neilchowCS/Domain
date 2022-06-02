using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoeTargetingExtension;

public class JoeBU : BattleUnit
{
    public JoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side, unitData)
    {

    }

    public JoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void SpawnProjectile(int i)
    {
        if (currentTarget != null)
        {
            BattleProjectile x = null;
            switch (i)
            {
                case 0:
                    x = new BattleProjectile(executor, side, this, i, currentTarget);
                    break;
                case 1:
                    x = new JoeSkillBP(executor, side, this, i, this.GetAoeLocation(3, 0));
                    break;
            }
        }
    }
}
