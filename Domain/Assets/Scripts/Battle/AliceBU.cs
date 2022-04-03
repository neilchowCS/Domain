using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBU : BattleUnit
{
    public AliceBU(BattleExecutor exec, int side)
        : base(exec, side, 0, "Alice", 10, 1, 20f, 4f, .1f)
    {
        
    }
}
