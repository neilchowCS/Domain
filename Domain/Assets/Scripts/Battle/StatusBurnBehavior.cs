using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBurnBehavior : StatusBehavior
{
    private float timer = 0;
    private float lifetime = 0;

    public StatusBurnBehavior(IBattleStatus host):base(host)
    {

    }

    public override void OnTickUp()
    {
        timer++;
        lifetime++;
        if (timer >= TickSpeed.ticksPerSecond)
        {
            Burn();
            timer -= TickSpeed.ticksPerSecond;
        }
        if (lifetime >= TickSpeed.ticksPerSecond * status.StatusData.duration)
        {
            //Debug.Log($"Burn expired {lifetime} ticks");
            OnUnapply();
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        Burn();
    }

    public void Burn()
    {
        if (status.Source != null)
        {
            Debug.Log((int)status.StatusData.value0);
            status.Executor.commandQueue.Enqueue(new() {
                new DamageCommand(status.Source, status.Host,
                (int)status.StatusData.value0, DamageType.special)
            });
        }
    }
}
