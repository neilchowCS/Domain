using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedObject : MonoBehaviour, IBattleObject
{
    public BattleExecutor Executor { get; set; }

    [field: SerializeField]
    public string ObjectName { get; set; }

    [field: SerializeField]
    public int GlobalObjectId { get; set; }

    [field: SerializeField]
    public int Side { get; set; }

    [field: SerializeField]
    public BattleObjectType ObjectType { get; set; }

    public virtual void Initialize(BattleExecutor exec, BattleObjectType objType, int side, string name)
    {
        Executor = exec;
        Side = side;
        ObjectType = objType;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        exec.AddObject(this);
    }

    public virtual void OnStartTurn() { }
    public virtual void OnEndTurn() { }

    public virtual void OnDamageDealt(IBattleObject damageSource, IBattleUnit damageTarget, int amount, DamageType damageType, AbilityType abilityType, bool isCrit, int overkill) { }
    public virtual void OnUnitDeath(IBattleUnit deadUnit) { }
    public virtual void OnHealApplied(IBattleObject healSource, IBattleUnit healTarget, int amount) { }
    public virtual void OnSpawn(IBattleObject source) { }
}
