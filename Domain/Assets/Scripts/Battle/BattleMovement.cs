using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class BattleMovement
{
    /// <summary>
    /// sets target tile
    /// adds to timeline
    /// </summary>
    public static void PrepareMovement(BattleUnit unit)
    {
        unit.targetTile = BUnitHelperFunc.GetNextBattleTile(unit, unit.currentTarget);

        unit.executor.timeline.AddTimelineEvent(new TimelineMove(unit.globalObjectId,
        0, unit.targetTile.position.x, unit.targetTile.position.y, unit.targetTile.position.z));

        if (unit.targetTile.occupied)
        {
            Debug.Log("Uh oh!");
        }

        unit.targetTile.occupied = true;
    }

    /// <summary>
    /// Find target.
    /// Check if in range or not.
    /// Change states and prepare movement.
    /// Decision to place state in range.
    /// Contains timeline update.
    /// </summary>
    public static void TargetDecision(BattleUnit unit)
    {
        unit.LookForward();
        if (BUnitHelperFunc.GetBattleUnitDistance(unit, unit.currentTarget) <= unit.unitData.unitRange)
        {
            unit.moveState = BattleUnit.MoveStates.inRange;
        }
        else
        {
            //FIX ME
            unit.moveState = BattleUnit.MoveStates.movingToTile;
            BattleMovement.PrepareMovement(unit);
        }
    }

    /// <summary>
    /// Movement loop
    /// </summary>
    public static void MoveTowardsNext(BattleUnit unit)
    {
        unit.position = Vector3.MoveTowards(unit.position, unit.targetTile.position, unit.unitData.unitMoveSpeed/TickSpeed.ticksPerSecond);
        if (Vector3.Distance(unit.position, unit.currentTile.position)
            < Vector3.Distance(unit.position, unit.targetTile.position))
        {
            unit.currentTile.occupied = false;
            unit.currentTile = unit.targetTile;
        }
        if (Vector3.Distance(unit.position, unit.targetTile.position) < 0.000001f)
        {
            unit.targetTile = null;
            if (BUnitHelperFunc.GetBattleUnitDistance(unit, unit.currentTarget) <= unit.unitData.unitRange)
            {
                unit.moveState = BattleUnit.MoveStates.inRange;
            }
            else
            {
                unit.moveState = BattleUnit.MoveStates.tileArrived;
            }

        }
    }
}
