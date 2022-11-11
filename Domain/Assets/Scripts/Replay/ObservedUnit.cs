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

    public int X { get; set; }
    public int Y { get; set; }

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
            PerformAttack();
        }
    }

    /// <summary>
    /// Move to next tile
    /// </summary>
    public void MoveTowardsNext()
    {
        (int, int) temp = (X,Y);
        Executor.hexMap[X, Y].occupied = false;

        (int, int) newTile = this.GetNextBattleTile();
        X = newTile.Item1;
        Y = newTile.Item2;
        Executor.hexMap[newTile.Item1, newTile.Item2].occupied = true;

        //unit.Position = unit.Executor.mapGraph[unit.Tile].Position;
        float time = .3f;
        movementController.StartMovement(Executor.hexMap[X, Y].Position,
            Vector3.Distance(Executor.hexMap[X, Y].Position, Position) / time);

        Executor.logger.AddMovement(this, X, Y);
    }

    public virtual void PerformAttack()
    {
        if (this.TargetInRange())
        {
            if (UnitData.mana >= UnitData.unitMaxMana.Value)
            {
                waitProjectile = true;
                //SKILL
                Executor.logger.AddAttack(this, 1, CurrentTarget);
                NewProjectile(1);
                ModifyMana(-UnitData.mana);
            }
            else
            {
                waitProjectile = true;
                //BASIC
                Executor.logger.AddAttack(this, 0, CurrentTarget);
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
        Executor.mapTilesObj[CurrentTarget.X][CurrentTarget.Y].SetRed();
        ActionExtension.ActionExtension.DealDamage(this, new() { CurrentTarget },
    (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[0].value0),
    DamageType.normal);
    }

    public virtual void SkillProjectileEffect()
    {
        Executor.mapTilesObj[CurrentTarget.X][CurrentTarget.Y].SetRed();
        ActionExtension.ActionExtension.DealDamage(this, new() { CurrentTarget },
    (int)(UnitData.unitAttack.Value * UnitData.baseData.attackDataList[1].value0),
    DamageType.normal);
    }

    public override void OnUnitDeath(IBattleUnit deadUnit)
    {
        if (deadUnit == this)
        {
            Executor.hexMap[X, Y].occupied = false;
            this.gameObject.SetActive(false);
            GameObject.Destroy(healthBar.gameObject);
        }

        if (deadUnit == CurrentTarget)
        {
            CurrentTarget = null;
        }
    }
}