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
    public BobBU(BattleExecutor exec, int side)
        : base(exec, side, 0, "Bob", 15, 1, 20f, 2f, .1f)
    {

    }
}
