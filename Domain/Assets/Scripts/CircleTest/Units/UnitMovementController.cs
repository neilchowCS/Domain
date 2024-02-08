using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementController : MonoBehaviour
{
    public ObservedUnit unit;
    public Vector3 destination;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(unit.Position, destination) <= 0.001f)
        {
            unit.Position = destination;
            this.enabled = false;
            unit.PerformAttack();
        }
        else
        {
            unit.Position = Vector3.MoveTowards(unit.Position, destination, speed * Time.deltaTime);
        }
    }

    public void StartMovement(Vector3 destination, float speed)
    {
        this.enabled = true;
        this.destination = destination;
        this.speed = speed;
    }
}
