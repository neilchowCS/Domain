using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public ReplayUnit parent;
    public Image healthFill;
    public Image manaFill;

    public List<StatusIcon> icons;

    void Awake()
    {
        icons = new List<StatusIcon>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
            healthFill.fillAmount = parent.unitData.health / (float)parent.unitData.unitMaxHealth.Value;
            manaFill.fillAmount = parent.unitData.mana / (float)parent.unitData.unitMaxMana.Value;
            this.transform.position =
                Camera.main.WorldToScreenPoint(parent.transform.position + new Vector3(0,2.5f,0));
        }
        
    }

    public void AddStatusIcon(StatusIcon i)
    {
        //fix me
        icons.Add(Instantiate(i, this.transform));
    }
}
