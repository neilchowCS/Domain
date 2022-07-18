using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Attribute
{
    protected float constantModifier;
    protected float coefficientModifier = 1;

    public virtual void ModifyAdditive(float i)
    {
        constantModifier += i;
    }

    public virtual void ModifyMultiplicative(float i)
    {
        coefficientModifier += i;
    }

    public virtual float GetCoefficient()
    {
        return coefficientModifier;
    }
}
