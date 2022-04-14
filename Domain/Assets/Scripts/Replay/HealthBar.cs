using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public ReplayUnit parent;
    public Image healthFill;
    public Image manaFill;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = GameObject.FindObjectOfType<Canvas>().transform;
        this.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (parent == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            float shrinkFactor = parent.currentHealth / (float)parent.unitData.unitHealth;
            healthFill.fillAmount = shrinkFactor;
            this.transform.position =
                Camera.main.WorldToScreenPoint(parent.transform.position + new Vector3(0,2.5f,0));
        }
        
    }
}
