using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorExtension;

public class ObservedUnit : ObservedObject, IBattleUnit
{
    //********************* IBattleUnit interface *************************
    //[field: Header(" ")]
    [field: SerializeField]
    public UnitRuntimeData UnitData { get; set; }

    [field: SerializeField]
    public float Timeline { get; set; }
    public Vector3 Position
    {
        get => this.transform.position;
        set
        {
            this.transform.position = value;
            healthBar.ChangePosition();
        }
    }

    public int Tile { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    [field: SerializeField, SerializeReference, Header("")]
    public List<IBattleStatus> StatusList { get; set; }

    //********************* IBattleUnit interface *************************

    //********************* Observed Specific *****************************
    public HealthBar healthBar;
    public UnitMovementController movementController;
    //********************* Observed Specific *****************************

    public virtual void ModifyHealth(int amount, DamageType damageType, IBattleUnit source)
    {
        UnitData.health += amount;
        healthBar.RefreshFill();
    }

    public virtual void ModifyMana(int amount)
    {
        UnitData.mana += amount;
        healthBar.RefreshFill();
    }

    public virtual void PerformAction()
    {
        bool hasMoved = false;
        bool hasAttacked = false;
        PerformMovement(ref hasMoved);
        PerformAttack(ref hasAttacked);
        if (hasMoved && !hasAttacked)
        {
            Timeline = Executor.maxTimeline - (Executor.maxTimeline * UnitData.unitRecovery.Value);
        }
        else
        {
            Timeline = Executor.maxTimeline;
        }
    }

    public virtual void PerformMovement(ref bool hasMoved)
    {
        if (CurrentTarget == null)
        {
            this.TargetClosestEnemy();
        }
        //target can't be null because it is set at start of function
        if (!this.TargetInRange())
        {
            this.TargetClosestEnemy();
            MoveTowardsNext();
            hasMoved = true;
        }
    }

    /// <summary>
    /// Move to next tile
    /// </summary>
    public void MoveTowardsNext()
    {
        Executor.mapGraph[Tile].occupied = false;

        Tile = this.GetNextBattleTile();
        Executor.mapGraph[Tile].occupied = true;

        //unit.Position = unit.Executor.mapGraph[unit.Tile].Position;
        float time = .7f;
        movementController.StartMovement(Executor.mapGraph[Tile].Position,
            Vector3.Distance(Executor.mapGraph[Tile].Position, Position) / time);
    }

    public virtual void PerformAttack(ref bool hasAttacked)
    {
        if (this.TargetInRange())
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                PerformSkill();
                ModifyMana(-UnitData.mana);
            }
            else
            {
                PerformBasic();
                ModifyMana(1);
            }
        }
    }

    public virtual void PerformBasic()
    {
        ActionExtension.DealDamage(this, new() { CurrentTarget },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
            DamageType.normal);
    }

    public virtual void PerformSkill()
    {
        ActionExtension.DealDamage(this, new() { CurrentTarget },
            (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
            DamageType.normal);
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == this)
        {
            Executor.mapGraph[Tile].occupied = false;
            this.gameObject.SetActive(false);
            GameObject.Destroy(healthBar.gameObject);
        }

        if (deadUnit == CurrentTarget)
        {
            CurrentTarget = null;
        }
    }
}