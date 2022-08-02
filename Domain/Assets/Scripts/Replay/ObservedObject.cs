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

    public virtual ObjectBehavior Behavior { get; set; }

    public virtual void Initialize(BattleExecutor exec, int side, string name)
    {
        Executor = exec;
        Side = side;

        GlobalObjectId = Executor.SetGlobalObjectId();
        ObjectName = name;

        Executor.GetAlliedObjects(this).Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
