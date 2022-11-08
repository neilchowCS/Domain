using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedProjectile : MonoBehaviour
{
    public ObservedUnit source;
    public int projectileId;
    public IBattleUnit target;
    public Vector3 targetLocation;
    public float speed;
    public bool unitTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (unitTarget && target != null)
        {
            targetLocation = target.Position;
        }
        else if (unitTarget)
        {
            Destroy(this.gameObject);
        }

        transform.up = targetLocation - transform.position;
        transform.position = Vector3.MoveTowards(transform.position,
            targetLocation + new Vector3(0, 1, 0),
            Time.deltaTime * speed);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(targetLocation.x, targetLocation.z)) < 0.0001f)
        {
            source.ProjectileHit(projectileId);
            Destroy(this.gameObject);
        }
    }
}
