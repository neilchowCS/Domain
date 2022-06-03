using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeBU : BattleUnit
{
    public DoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side, unitData)
    {

    }

    public DoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
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
                    x = new DoeSkillBP(executor, side, this, i, position);
                    break;
            }
        }
    }
}
