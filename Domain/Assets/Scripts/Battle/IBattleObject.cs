using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleObject
{
    public BattleExecutor Executor { get; set; }
    public int Side { get; set; }

    public int GlobalObjectId { get; }
    public string ObjectName { get; set; }

    public ObjectBehavior Behavior { get; }
}
