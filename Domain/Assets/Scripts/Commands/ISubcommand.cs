using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubcommand
{
    public float Yield { get; set; }
    public void Execute();
}
