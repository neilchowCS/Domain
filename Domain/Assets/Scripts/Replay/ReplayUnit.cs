using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUnit : ReplayObject 
{
    public ReplayExecutor executor;
    public int side = 0;

    [Header(" ")]
    public UnitRuntimeData unitData;


    [Header(" ")]
    public HealthBar healthBar;

    public ReplayObject target;
    public Vector3 destination;
    public bool moving;

    // Start is called before the first frame update
    public virtual void Start()
    {
        moving = false;
        healthBar = Instantiate(unitData.baseData.commonRef.healthBarPrefab,
            executor.replayManager.screenOverlayCanvas.transform, false);
        if (this.transform.position.x >= 0)
        {
            healthBar.healthFill.GetComponent<Image>().color = Color.red;
        }
        //healthBar.parent = this;
        Instantiate(unitData.baseData.commonRef.warpParticle, this.transform.position,
            unitData.baseData.commonRef.warpParticle.transform.rotation);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * unitData.unitRange.Value);
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
            float MoveSpeed = 0;//unitData.unitMoveSpeed.Value;
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                destination, Time.deltaTime * MoveSpeed);
        }
    }
}
