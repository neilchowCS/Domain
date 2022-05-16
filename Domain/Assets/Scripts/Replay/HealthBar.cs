using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public void AddStatusIcon(StatusIcon i, Sprite statusSprite)
    {
        List<int> listId = new List<int>();
        int iconIndex = 0;
        bool exist = false;
        foreach(StatusIcon j in icons)
        {
            if (!listId.Contains(j.id))
            {
                listId.Add(j.id);
            }
        }

        if (listId.Contains(i.id))
        {
            exist = true;
            iconIndex = listId.IndexOf(i.id);
        }

        //fix me
        if (!exist)
        {
            icons.Add(Instantiate(i, this.transform));
            icons[icons.Count - 1].GetComponent<Image>().sprite = statusSprite;
            icons[icons.Count - 1].transform.position = icons[icons.Count - 1].transform.position +
                new Vector3(40 * (icons.Count - 1f), 0, 0);
            iconIndex = icons.Count - 1;
        }
        else
        {
            icons[iconIndex].number.gameObject.SetActive(true);
            icons[iconIndex].count++;
            icons[iconIndex].number.text = icons[0].count + "";
        }
        
    }
}
