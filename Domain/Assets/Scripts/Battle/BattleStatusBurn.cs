using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusBurn : BattleStatus
{
    public BattleUnit host;
    public BattleUnit source;
    public int damagePerTick;
    public float seconds;
    private float timer = 0;

    public BattleStatusBurn(BattleUnit host, BattleUnit source, int damagePerTick, float seconds)
        : base(host.executor, source.side, 1, "Burn")
    {
        this.host = host;
        this.source = source;
        this.damagePerTick = damagePerTick;
        this.seconds = seconds;

        OnApply();
    }

    public void OnApply()
    {
        executor.DealDamage(source, host, damagePerTick);
    }

    public void OnUnapply()
    {
        host.statusList.Remove(this);
    }
}
