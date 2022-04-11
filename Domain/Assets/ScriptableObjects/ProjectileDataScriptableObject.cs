using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProjectileDataSO", order = 1)]
public class ProjectileDataScriptableObject : ScriptableObject
{
    public ReplayProjectile projectile;
    public bool travel;
    public bool followTarget;
    //
    public float delay;
    public float speed;
}
