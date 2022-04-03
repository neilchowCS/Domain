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
    public AliceBU(BattleExecutor exec, int side)
        : base(exec, side, 0, "Alice", 10, 1, 20f, 4f, .1f)
    {
        
    }
}
