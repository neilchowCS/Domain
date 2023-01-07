using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventCommand
{
    public void Execute(IBattleObject obj);
}
