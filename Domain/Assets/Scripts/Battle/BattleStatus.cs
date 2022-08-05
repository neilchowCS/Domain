using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : BattleObject, IBattleStatus
{
    public SimpleStatusData StatusData { get; set; }
    public IBattleUnit Host { get; set; }
    public IBattleUnit Source { get; set; }

    public BattleStatusActions Actions { get; set; }

    public BattleStatus(BattleExecutor exec, string name,
        IBattleUnit host, IBattleUnit source, SimpleStatusData data)
        :base(exec, source.Side, name){

        this.Host = host;
        this.Source = source;
        this.StatusData = data;
    }

    ~BattleStatus()
    {
        Debug.Log($"{ObjectName} garbage collected");
    }
}