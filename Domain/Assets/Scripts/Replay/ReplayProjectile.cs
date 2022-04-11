using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayProjectile : MonoBehaviour
{
    public ReplayUnit target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                5 * 1f / TickSpeed.ticksPerSecond);
            if (Vector3.Distance(transform.position, target.transform.position) < 0.00001f)
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
