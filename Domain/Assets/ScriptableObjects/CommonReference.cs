using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CommonRef", order = 1)]
public class CommonReference : ScriptableObject
{
    public HealthBar healthBarPrefab;
    public GameObject warpParticle;

    public StatusIcon statusIconPrefab;

    public List<Sprite> statusIconList;

    public List<Sprite> elementIconList;

    public ObservedStatusBurn observedBurn;
}
