using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnCommand : IEventCommand
{
    public int Id { get; set; } = 1;

    public EndTurnCommand()
    {

    }

    /// <summary>
    /// Raises DealDamage event.
    /// </summary>
    public void Execute(IBattleObject obj)
    {
        obj.OnEndTurn();
    }
}
