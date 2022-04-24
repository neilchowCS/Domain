using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeFloat : Attribute
{
    private float value;
    //return
    public float Value { get { return value + modifier; } }

    public AttributeFloat()
    {

    }

    public AttributeFloat(float i)
    {
        value = i;
    }

    public void SetValue(float i)
    {
        value = i;
    }
}
