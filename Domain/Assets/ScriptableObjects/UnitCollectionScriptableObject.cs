using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitCollection", order = 1)]
public class UnitCollectionScriptableObject : ScriptableObject
{
    public List<UnitIndividualData> collectionList;
}
