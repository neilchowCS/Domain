using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedStatusFramework : ObservedObject, IBattleStatus
{
    public IBattleUnit Host { get; set; }
    public IBattleObject Source { get; set; }
    public StatusType statusType;
    public bool hasDuration;
    public int duration;
    public List<ParticleSystem> particleSystems;

    public virtual void Initialize(BattleExecutor executor, string name, IBattleObject source,
        IBattleUnit host, StatusType type, int duration)
    {
        Initialize(executor, source.Side, name, host);

        Source = source;
        Host = host;
        hasDuration = true;
        this.duration = duration;
        statusType = type;

        Attach();
    }

    public virtual void Attach()
    {
        if (Host is ObservedUnit)
        {
            foreach (ParticleSystem pSys in particleSystems)
            {
                pSys.gameObject.transform.parent = ((ObservedUnit)Host).transform;
                pSys.transform.localPosition = Vector3.zero;
            }
        }
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == Host)
        {
            Executor.eventManager.RemoveObject(this);
        }
    }

    public override void OnEndTurn()
    {
        if (hasDuration && Executor.actingUnit == Host)
        {
            duration--;
            if (duration <= 0)
            {
                Executor.eventManager.RemoveObject(this);
            }
        }
    }

    public virtual void RemoveStatus()
    {
        OnUnapply();
        Host.StatusList.Remove(this);
        foreach(ParticleSystem p in particleSystems)
        {
            Destroy(p);
        }
        Destroy(this.gameObject);
    }

    public virtual void OnUnapply()
    {

    }
}
