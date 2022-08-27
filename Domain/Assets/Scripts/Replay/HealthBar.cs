using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HealthBar : MonoBehaviour
{
    public ObservedUnit parent;
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

    public void ChangePosition()
    {
        this.transform.position =
                   Camera.main.WorldToScreenPoint(parent.Position + new Vector3(0, 2.8f, 0));
    }

    public void AddStatus(StatusIcon i, Sprite statusSprite, int statusId)
    {
        List<int> listId = new List<int>();
        int iconIndex = 0;
        bool exist = false;
        foreach (StatusIcon j in icons)
        {
            if (!listId.Contains(j.id))
            {
                listId.Add(j.id);
            }
        }

        if (listId.Contains(statusId))
        {
            exist = true;
            iconIndex = listId.IndexOf(statusId);
        }

        //fix me
        if (!exist)
        {
            icons.Add(Instantiate(i, this.transform));
            icons[^1].count = 1;
            icons[^1].id = statusId;
            icons[^1].GetComponent<Image>().sprite = statusSprite;
            icons[^1].transform.position = icons[^1].transform.position +
                new Vector3(40 * (icons.Count - 1), 0, 0);
        }
        else
        {
            icons[iconIndex].count++;
            icons[iconIndex].number.text = icons[iconIndex].count + "";
        }
    }

    public void RemoveStatus(int statusId)
    {
        foreach (StatusIcon i in icons)
        {
            if (i.id == statusId)
            {
                if (i.count == 1)
                {
                    icons.Remove(i);
                    Destroy(i.gameObject);
                    return;
                }
                else
                {
                    i.count -= 1;
                    i.number.text = i.count + "";
                    return;
                }
            }
        }
    }

    public void RefreshStatusDisplay()
    {

    }
}
