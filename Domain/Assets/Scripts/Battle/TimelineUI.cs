using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineUI : MonoBehaviour
{
    public List<BaseUnitIcon> timelineIcons;
    public BaseUnitIcon prefabIcon;
    public ObservedBattleExecutor executor;
    private float constantDisplacement = 12.6f;
    private float vector = .35f;

    //Call set timeline after setting initial speed position
    public void InitTimeline(List<IBattleUnit> units)
    {
        //float constant = 63f/ 5f;
        foreach (IBattleUnit unit in units)
        {
            BaseUnitIcon x = GameObject.Instantiate(prefabIcon, this.gameObject.transform, false);
            timelineIcons.Add(x);
            x.InitButton(executor.dataListSO, unit.UnitData.individualData);
            x.levelText.text = "";
            x.transform.localScale = new Vector3(vector, vector, vector);
            x.transform.localPosition = new Vector3((unit.Timeline - (executor.maxTimeline / 2f)) * constantDisplacement, 0, 0);
        }
    }

    public void RefreshTimeline()
    {
        //float constant = 63f / 5f;
        for (int i = 0; i < executor.activeUnits.Count; i++)
        {
            timelineIcons[i].InitButton(executor.dataListSO, executor.activeUnits[i].UnitData.individualData);
            timelineIcons[i].levelText.text = "";
            timelineIcons[i].transform.localPosition = new Vector3((executor.activeUnits[i].Timeline - (executor.maxTimeline / 2f)) * constantDisplacement, 0, 0);
        }
        while(executor.activeUnits.Count < timelineIcons.Count)
        {
            Destroy(timelineIcons[^1].gameObject);
            timelineIcons.RemoveAt(timelineIcons.Count - 1);
        }
    }
}
