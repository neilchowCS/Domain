using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleBehaviorExtension;
using AoeTargetingExtension;

/// <summary>
/// Inherits from BattleObject, IBattleUnit. Unobserved (server side).
/// </summary>
public class BattleUnit : BattleObject, IBattleUnit
{
    public UnitRuntimeData UnitData { get; set; }

    public BattleUnitActions Actions { get; set; }

    public float Timeline { get; set; }
    public Vector3 Position { get; set; }

    public int Tile { get; set; }

    public IBattleUnit CurrentTarget { get; set; } = null;

    public bool NeedsCleaning { get; set; } = false;

    public int ManaCounter { get; set; } = 0;

    public List<IBattleStatus> StatusList { get; set; }

    /// <summary>
    /// BattleUnit constructor
    /// </summary>
    public BattleUnit(BattleExecutor exec, int side,
        UnitRuntimeData unitData, int tileId)
        : base(exec, side, unitData.baseData.unitName)
    {
        this.UnitData = unitData;
        StatusList = new();

        Tile = tileId;
        Timeline = 0;
        Position = Executor.mapGraph[Tile].Position;
        Executor.mapGraph[Tile].occupied = true;
    }
}