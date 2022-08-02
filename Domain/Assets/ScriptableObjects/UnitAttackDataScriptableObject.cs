using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitAttackDataSO", order = 1)]
public class UnitAttackDataScriptableObject : ScriptableObject
{
    public string Name;

    public float radius;
    public float speed;

    public float leadup;
    public float backswing;

    public float value0;
    public float value1;
    public float value2;
    public float value3;
}
