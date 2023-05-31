using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteStore", order = 1)]
public class SpriteStoreScriptableObject : ScriptableObject
{
    public List<Sprite> sprites;
}