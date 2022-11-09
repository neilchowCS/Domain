using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionExtension;

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
    protected bool hasMoved;
    protected bool hasAttacked;
    protected bool waitProjectile;
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
        hasMoved = false;
        hasAttacked = false;
        waitProjectile = false;
        PerformMovement();
    }

    public virtual void PerformMovement()
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
        else
        {
            Debug.Log("perform attack in range");
            PerformAttack();
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
        float time = .3f;
        movementController.StartMovement(Executor.mapGraph[Tile].Position,
            Vector3.Distance(Executor.mapGraph[Tile].Position, Position) / time);
    }

    public virtual void PerformAttack()
    {
        if (this.TargetInRange())
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                waitProjectile = true;
                //SKILL
                NewProjectile(1);
                ModifyMana(-UnitData.mana);
            }
            else
            {
                waitProjectile = true;
                //BASIC
                NewProjectile(0);
                ModifyMana(1);
            }
            hasAttacked = true;
        }

        if (!waitProjectile)
        {
            EndActionCycle();
        }
    }

    public virtual void EndActionCycle()
    {
        if (hasMoved && !hasAttacked)
        {
            Debug.Log("recovery");
            Timeline = Executor.maxTimeline - (Executor.maxTimeline * UnitData.unitRecovery.Value);
        }
        else
        {
            Timeline = Executor.maxTimeline;
        }
        Executor.StepUp();
    }

    public virtual void NewProjectile(int index)
    {
        Executor.factory.GetObservedProjectile(this, index);
    }

    public virtual void ProjectileHit(int id)
    {
        switch (id)
        {
            case 0:
                BasicProjectileEffect();
                break;
            case 1:
                SkillProjectileEffect();
                break;
        }
        EndActionCycle();
    }

    public virtual void BasicProjectileEffect()
    {
        Executor.mapTilesObj[CurrentTarget.Tile].SetRed();
        ActionExtension.ActionExtension.DealDamage(this, new() { CurrentTarget },
    (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
    DamageType.normal);
    }

    public virtual void SkillProjectileEffect()
    {
        Executor.mapTilesObj[CurrentTarget.Tile].SetRed();
        ActionExtension.ActionExtension.DealDamage(this, new() { CurrentTarget },
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