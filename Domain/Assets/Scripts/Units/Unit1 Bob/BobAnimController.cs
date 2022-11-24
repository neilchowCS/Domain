using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAnimController : MonoBehaviour
{
    public ObservedBob host;
    public ParticleSystem thornsParticle;

    public void CreateThorns()
    {
        thornsParticle.Play();
    }
}
