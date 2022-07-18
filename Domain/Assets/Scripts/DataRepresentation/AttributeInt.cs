using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeInt : Attribute
{
    [SerializeField]
    private int value;
    //return
    public int Value { get { return (int)(coefficientModifier * value + constantModifier); } }

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
