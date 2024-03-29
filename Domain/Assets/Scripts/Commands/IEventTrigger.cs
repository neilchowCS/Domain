using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventTrigger
{
    public int Id { get; set; }
    public void Execute(IBattleObject obj);
}
