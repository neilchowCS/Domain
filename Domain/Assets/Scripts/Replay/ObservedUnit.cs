using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedUnit : ObservedObject, IBattleUnit
{
    //********************* IBattleUnit interface *************************
    //[field: Header(" ")]
    [field: SerializeField]
    public UnitRuntimeData UnitData { get; set; }

    //public override ObjectBehavior Behavior { get; set; }
    public BattleUnitActions Actions { get; set; }

    public Vector3 Position
    {
        get => this.transform.position;
        set
        {
            this.transform.position = value;
            healthBar.ChangePosition();
        }
    }

    public BattleTile CurrentTile { get; set; }
    public BattleTile TargetTile { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    public bool NeedsCleaning { get; set; } = false;

    public bool IsMoving { get; set; } = false;

    public AttackStates AttackState { get; set; } = AttackStates.idle;
    public float TickOfLastAttack { get; set; } = 0;
    public float AttackTimer { get; set; } = 0;

    public int ManaCounter { get; set; } = 0;

    [field: SerializeField, SerializeReference, Header("")]
    public List<IBattleStatus> StatusList { get; set; }

    //********************* IBattleUnit interface *************************

    //********************* Observed Specific *****************************
    public HealthBar healthBar;
    //********************* Observed Specific *****************************

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
