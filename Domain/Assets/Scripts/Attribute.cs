using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attribute
{
    protected float modifier;

    public virtual void Modify(float i)
    {
        modifier += i;
    }

}
