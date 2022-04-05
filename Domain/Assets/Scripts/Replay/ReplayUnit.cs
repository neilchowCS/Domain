using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayUnit : ReplayObject
{
    public ReplayObject target;
    public Vector3 destination;
    public bool moving;
    // Start is called before the first frame update
    public virtual void Start()
    {
        moving = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 4);
        if (target != null)
        {
            Vector3 direction = (target.transform.position - this.transform.position).normalized;
            
            if (this.transform.forward != direction)
            {
                float RotationSpeed = 10f;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                this.transform.rotation =
                    Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            }
        }
        if (moving)
        {
            float MoveSpeed = 1f;
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                destination, Time.deltaTime * MoveSpeed);
        }
    }
}
