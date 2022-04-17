using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UDListSO", order = 1)]
public class UDListScriptableObject : ScriptableObject
{
    public List<UnitDataScriptableObject> uDList;
}