using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleStatus : IBattleObject
{
    public IBattleUnit Host { get; set; }
    public BattleStatusActions Actions { get; set; }
}
