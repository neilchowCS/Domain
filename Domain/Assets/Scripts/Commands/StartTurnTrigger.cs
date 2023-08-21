using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnTrigger : IEventTrigger
{
    public int Id { get; set; }

    public StartTurnTrigger()
    {
        Id = 0;
    }

    public void Execute(IBattleObject obj)
    {
        obj.OnStartTurn();
    }
}

