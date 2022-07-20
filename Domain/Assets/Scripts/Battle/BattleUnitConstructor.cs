using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class BattleUnitConstructor
{
    public static BattleUnit GetBattleUnit(BattleExecutor exec, int side, UnitRuntimeData data, int tileId)
    {
        BattleUnit output = null;
        switch (data.baseData.unitId)
        {
            case 0:
                output = new AliceBU(exec, side, data, tileId);
                break;
            case 1:
                output = new BobBU(exec, side, data, tileId);
                break;
            case 2:
                output = new JoeBU(exec, side, data, tileId);
                break;
            case 3:
                output = new DoeBU(exec, side, data, tileId);
                break;
        }
        return output;
    }

    public static ObservedUnit GetObservedUnit()
    {
        return null;
    }
}