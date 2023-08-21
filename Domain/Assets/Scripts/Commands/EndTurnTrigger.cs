using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnTrigger : IEventTrigger
{
    public int Id { get; set; }

    public EndTurnTrigger()
    {
        Id = 1;
    }

    public void Execute(IBattleObject obj)
    {
        obj.OnEndTurn();
    }
}

