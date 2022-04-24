using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeInt : Attribute
{
    private int value;
    //return
    public int Value { get { return value + (int)modifier; } }

    public AttributeInt()
    {

    }

    public AttributeInt(int i)
    {
        value = i;
    }

    public void SetValue(int i)
    {
        value = i;
    }
}
