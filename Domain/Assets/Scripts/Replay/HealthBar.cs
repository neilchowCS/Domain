using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public ReplayUnit parent;
    public GameObject child;
    private float initialTransform;

    // Start is called before the first frame update
    void Start()
    {
        initialTransform = child.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            float shrinkFactor = parent.currentHealth / (float)parent.unitData.baseHealth;
            this.transform.position = new Vector3(parent.transform.position.x,
            this.transform.position.y, parent.transform.position.z);
            child.transform.localScale = new Vector3 (initialTransform * shrinkFactor,
                child.transform.localScale.y, child.transform.localScale.z);
        }
        
    }
}
