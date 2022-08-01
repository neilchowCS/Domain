using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleStatus : IBattleObject
{
    public SimpleStatusData StatusData { get; set; }
    public IBattleUnit Host { get; set; }
    public IBattleUnit Source { get; set; }
    public BattleStatusActions Actions { get; set; }
}
