using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoeTargetingExtension;

public class JoeBU : BattleUnit
{
    /// <summary>
    /// Constructor for Joe BattleUnit (Unobserved).
    /// </summary>
    public JoeBU(BattleExecutor exec, int side, UnitRuntimeData unitData, int tileId)
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
                    x = new JoeSkillBP(Executor, Side, this, i, this.GetAoeLocation(3, 0));
                    break;
            }
        }
    }
}
