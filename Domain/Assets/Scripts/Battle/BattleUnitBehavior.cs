using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitBehavior : MonoBehaviour
{
    BattleUnit unit;

    public BattleUnitBehavior(BattleUnit unit)
    {
        this.unit = unit;
    }

    public void SetPosition(Vector3 position)
    {
        unit.position = position;
    }
}
