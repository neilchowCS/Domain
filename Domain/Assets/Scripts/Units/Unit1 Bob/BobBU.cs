using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBU : BattleUnit
{
    /// <summary>
    /// Constructor for Bob BattleUnit (Unobserved).
    /// </summary>
    public BobBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData, tileId)
    {

    }

    public override void SpawnProjectile(int i)
    {
        if (CurrentTarget != null)
        {
            BattleProjectile x = null;
            switch (i)
            {
                case 0:
                    x = new BattleProjectile(Executor, Side, this, i, CurrentTarget);
                    break;
                case 1:
                    x = new AliceSkillBP(Executor, Side, this, i, CurrentTarget);
                    break;
            }
        }
    }
}
