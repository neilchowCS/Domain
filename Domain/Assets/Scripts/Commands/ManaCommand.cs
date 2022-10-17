using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCommand : ISubcommand
{
    private IBattleUnit host;
    private int amount;
    private bool isRatio;
    public float Yield { get; set; }

    public ManaCommand(IBattleUnit host, int amount, bool isRatio)
    {
        this.host = host;
        this.amount = amount;
        this.isRatio = isRatio;
        Yield = 0;
    }

    public void Execute()
    {
        int value = amount;
        if (isRatio)
        {
            value = host.UnitData.unitMaxMana.Value * amount;
            
        }

        if (value + host.UnitData.mana > host.UnitData.unitMaxMana.Value)
        {
            value = host.UnitData.unitMaxMana.Value;
        }else if (value + host.UnitData.mana < 0)
        {
            value = 0;
        }

        host.Actions.SetMana(value);
    }
}
