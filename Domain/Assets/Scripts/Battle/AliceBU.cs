using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBU : BattleUnit
{
    /// <summary>
    /// Constructor for Alice BattleUnit.
    /// </summary>
    /// <param name="exec"> BattleExecutor </param>
    /// <param name="side"> 0 or 1 </param>
    public AliceBU(BattleExecutor exec, int side, UnitData unitData)
        : base(exec, side, unitData)
    {
        
    }
}
