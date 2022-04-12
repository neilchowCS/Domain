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
            transform.up = target.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position,
                target.transform.position + new Vector3(0, 1, 0),
                target.unitData.baseData.projectileData[0].speed * 1f / TickSpeed.ticksPerSecond);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                new Vector2(target.transform.position.x, target.transform.position.z)) < 0.00001f)
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