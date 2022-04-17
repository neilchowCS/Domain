using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class BattleUnitConstructor
{

    public static BattleUnit GetBattleUnit(BattleExecutor exec, int side, UnitData data)
    {
        BattleUnit output = null;
        switch (data.baseData.unitId)
        {
            case 0:
                output = new AliceBU(exec, side, data);
                break;
            case 1:
                output = new BobBU(exec, side, data);
                break;
        }
        return output;
    }

    public static BattleUnit GetBattleUnit(BattleExecutor exec, int side, UnitData data, int tileId)
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
        }
        return output;
    }
}