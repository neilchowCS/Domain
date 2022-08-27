using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedBattleExecutor : BattleExecutor
{
    public override void Start()
    {
        //Instantiate(AudioSingleton.PrefabAudio);
        //ExecuteBattle();
        InitState();
        Debug.Log($"P0: {player0Active.Count}");
        Debug.Log($"P1: {player1Active.Count}");
        globalTick++;
    }

    public override void ExecuteBattle()
    {
        if (this.enabled == false)
        {
            InitState();
            Debug.Log($"P0: {player0Active.Count}");
            Debug.Log($"P1: {player1Active.Count}");
            globalTick++;
            this.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (ContinueRun())
        {
            StepUp();
        }
        else
        {
            Debug.Log(globalTick + " ticks");
            Debug.Log(player1Active.Count == 0 && player0Active.Count >= 1 ? "Player won!" : "Player lost!");
            this.enabled = false;
        }
    }

    protected override void InstantiateUnits()
    {
        for (int i = 0; i < reader.record.team0Data.Count; i++)
        {
            if (reader.record.team0Data.Count <= reader.record.team0Position.Count)
            {
                player0Active.Add(factory.NewObservedUnit(0,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team0Data[i].unitId], reader.record.team0Data[i])),
                    reader.record.team0Position[i]));
            }
        }

        for (int i = 0; i < reader.record.team1Data.Count; i++)
        {
            if (reader.record.team1Data.Count <= reader.record.team1Position.Count)
            {
                player1Active.Add(factory.NewObservedUnit(1,
                    new UnitRuntimeData((dataListSO.uDList[reader.record.team1Data[i].unitId], reader.record.team1Data[i])),
                    reader.record.team1Position[i]));
            }
        }
    }
}
