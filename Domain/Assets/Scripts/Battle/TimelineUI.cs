using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimelineUI : MonoBehaviour
{
    public List<TimelineIcon> timelineIcons;
    public TimelineIcon prefabIcon;
    public ObservedBattleExecutor executor;
    private float constantDisplacement = 12.6f;
    private float vector = .35f;

    //Call set timeline after setting initial speed position
    public void InitTimeline(List<IBattleUnit> units)
    {
        Debug.Log("init timeline");
        //float constant = 63f/ 5f;
        foreach (IBattleUnit unit in units)
        {
            TimelineIcon x = GameObject.Instantiate(prefabIcon, this.gameObject.transform, false);
            timelineIcons.Add(x);
            x.image.sprite = unit.UnitData.baseData.unitSprite;
            x.id = unit.GlobalObjectId;
            x.transform.localScale = new Vector3(vector, vector, vector);
            x.transform.localPosition = new Vector3((unit.Timeline - (executor.maxTimeline / 2f)) * constantDisplacement, 0, 0);
        }
    }

    public void RefreshTimeline()
    {
        //Debug.Log("refresh timeline");
        /*
        //float constant = 63f / 5f;
        for (int i = 0; i < executor.activeUnits.Count; i++)
        {
            //timelineIcons[i].InitButton(executor.dataListSO, executor.activeUnits[i].UnitData.individualData);
            //timelineIcons[i].levelText.text = "";
            //timelineIcons[i].transform.localPosition = new Vector3((executor.activeUnits[i].Timeline - (executor.maxTimeline / 2f)) * constantDisplacement, 0, 0);
        }
        while(executor.activeUnits.Count < timelineIcons.Count)
        {
            Destroy(timelineIcons[^1].gameObject);
            timelineIcons.RemoveAt(timelineIcons.Count - 1);
        }
        */
        List<TimelineIcon> destroy = new();
        foreach (TimelineIcon icon in timelineIcons)
        {
            IBattleUnit unit = executor.activeUnits.Find(o => o.GlobalObjectId == icon.id);
            if (unit == null)
            {
                destroy.Add(icon);
            }
            else
            {
                Vector3 newPos = new Vector3((unit.Timeline - (executor.maxTimeline / 2f)) * constantDisplacement, 0, 0);
                icon.StartMovement(newPos, Vector3.Distance(newPos, icon.transform.position)/0.8f);
            }
        }

        while (destroy.Count > 0)
        {
            TimelineIcon icon = destroy[0];
            destroy.Remove(icon);
            timelineIcons.Remove(icon);
            Destroy(icon.gameObject);
        }
    }
}