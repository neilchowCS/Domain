using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUnit : ReplayObject
{
    public int side = 0;
    public UnitData unitData;
    public HealthBar healthBar;

    public int currentHealth;
    public ReplayObject target;
    public Vector3 destination;
    public bool moving;

    // Start is called before the first frame update
    public virtual void Start()
    {
        moving = false;
        healthBar = Instantiate(unitData.baseData.healthBarPrefab);
        if (this.transform.position.x >= 0)
        {
            healthBar.child.GetComponent<Image>().color = Color.red;
        }
        healthBar.parent = this;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * unitData.unitRange);
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
            float MoveSpeed = unitData.unitMoveSpeed;
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                destination, Time.deltaTime * MoveSpeed);
        }
    }
}
