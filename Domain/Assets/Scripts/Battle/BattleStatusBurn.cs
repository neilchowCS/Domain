using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusBurn : BattleStatus
{
    public IBattleUnit source;
    public int damagePerTick;
    public float seconds;
    private float lifetime = 0;
    private float timer = 0;

    public BattleStatusBurn(IBattleUnit host, IBattleUnit source, int damagePerTick, float seconds)
        : base(host.Executor, source.Side, "BurnStatus", host)
    {
        this.source = source;
        this.damagePerTick = damagePerTick;
        this.seconds = seconds;

        //OnApply();
    }
    /*
    public override void OnTickUp()
    {
        timer++;
        lifetime++;
        if (timer >= TickSpeed.ticksPerSecond)
        {
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
            Executor.DealDamage(source, host, damagePerTick, DamageType.special);
        }
    }

    public override void OnUnapply()
    {
        if (host != null)
        {
            host.StatusList.Remove(this);
        }
        Executor.RemoveObject(this);
    }
    */
}
