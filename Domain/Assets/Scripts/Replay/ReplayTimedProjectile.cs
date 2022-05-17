using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayTimedProjectile : ReplayProjectile
{
    public float duration;
    public float timer;
    // Update is called once per frame
    void Update()
    {
        if (duration != 0)
        {
            timer += Time.deltaTime;
            if (duration <= timer)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
