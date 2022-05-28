using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusBurn : BattleStatus
{
    public BattleUnit source;
    public int damagePerTick;
    public float seconds;
    private float lifetime = 0;
    private float timer = 0;

    public BattleStatusBurn(BattleUnit host, BattleUnit source, int damagePerTick, float seconds)
        : base(host.executor, source.side, 1, "Burn", host)
    {
        this.source = source;
        this.damagePerTick = damagePerTick;
        this.seconds = seconds;

        OnApply();
    }

    public override void OnTickUp()
    {
        timer++;
        lifetime++;
        if (timer >= TickSpeed.ticksPerSecond)
        {
            Debug.Log(lifetime);
            OnApply();
            timer -= TickSpeed.ticksPerSecond;
        }
        if (lifetime >= TickSpeed.ticksPerSecond * seconds)
        {
            OnUnapply();
        }
    }

    public void OnApply()
    {
        if (host != null)
        {
            executor.DealDamage(source, host, damagePerTick);
        }
    }

    public override void OnUnapply()
    {
        if (host != null)
        {
            host.statusList.Remove(this);
        }
        executor.RemoveObject(this);
    }
}
