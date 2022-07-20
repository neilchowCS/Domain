using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeBU : BattleUnit
{
    /// <summary>
    /// Constructor for Doe BattleUnit (Unobserved).
    /// </summary>
    public DoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
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
                    x = new DoeSkillBP(Executor, Side, this, i, Position);
                    break;
            }
        }
    }
}
