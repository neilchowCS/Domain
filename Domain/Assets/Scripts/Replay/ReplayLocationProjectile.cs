using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayLocationProjectile : ReplayProjectile
{
    // Start is called before the first frame update
    void Start()
    {
        transform.up = targetLocation - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                targetLocation + new Vector3(0, 1, 0),
                Time.deltaTime * speed);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(targetLocation.x, targetLocation.z)) < 0.0001f)
        {
            Destroy(this.gameObject);
        }
    }
}
