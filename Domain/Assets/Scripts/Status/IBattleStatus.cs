using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleStatus : IBattleObject
{
    public IBattleUnit Host { get; set; }
    public IBattleObject Source { get; set; }
    public void RemoveStatus();
}
