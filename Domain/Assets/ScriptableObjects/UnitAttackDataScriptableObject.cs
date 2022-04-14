using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitAttackDataSO", order = 1)]
public class UnitAttackDataScriptableObject : ScriptableObject
{
    public string attackType;

    public ReplayProjectile projectile;
    public bool travel;
    public bool followTarget;

    public float speed;

    public float backswing;
}
