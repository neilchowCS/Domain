using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBU : BattleUnit
{
    /// <summary>
    /// Constructor for Bob BattleUnit.
    /// </summary>
    /// <param name="exec"> BattleExecutor </param>
    /// <param name="side"> 0 or 1 </param>
    public BobBU(BattleExecutor exec, int side, UnitRuntimeData unitData)
        : base(exec, side, unitData)
    {

    }

    public BobBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
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
                    x = new AliceSkillBP(executor, side, this, i, currentTarget);
                    break;
            }
        }
    }
}
