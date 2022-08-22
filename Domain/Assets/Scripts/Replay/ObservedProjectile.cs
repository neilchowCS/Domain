using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedProjectile : ObservedObject, IBattleProjectile
{
    //********************* IBattleProjectile interface *************************
    public BattleProjectileActions Actions { get; set; }

    public IBattleUnit Source { get; set; }
    public int SourceGlobalId { get; set; } = -1;

    public FixedUnitState UnitState { get; set; }

    public Vector3 Position
    {
        get => this.transform.position;
        set => this.transform.position = value;
    }
    public GameObject GetGameObject() => this.gameObject;

    public IBattleUnit TargetUnit { get; set; }
    public Vector3 TargetLocation { get; set; }
    public UnitAttackDataScriptableObject AttackData { get; set; }

    //********************* IBattleProjectile interface *************************

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
