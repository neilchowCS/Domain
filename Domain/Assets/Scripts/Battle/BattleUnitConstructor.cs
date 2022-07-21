using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class BattleUnitConstructor
{
    public static ObservedUnit GetObservedUnit()
    {
        return null;
    }

    public static UnitBehavior GetUnitBehavior(int unitId, IBattleUnit unit)
    {
        UnitBehavior output = null;
        switch (unitId)
        {
            case 0:
                output = new AliceBehavior(unit);
                break;
            case 1:
                output = new UnitBehavior(unit);
                break;
            case 2:
                output = new JoeBehavior(unit);
                break;
            case 3:
                output = new DoeBehavior(unit);
                break;
        }
        return output;
    }

    public static BattleUnitActions GetUnitActions(int unitId, IBattleUnit unit)
    {
        BattleUnitActions output = null;
        switch (unitId)
        {
            default:
                output = new BattleUnitActions(unit);
                break;
        }
        return output;
    }
}