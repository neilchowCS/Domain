using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline
{
    public BattleExecutor exec;

    public SortedDictionary<int, List<TimelineEvent>> timeEvent;

    public Timeline(BattleExecutor e)
    {
        exec = e;
        timeEvent = new SortedDictionary<int, List<TimelineEvent>>();
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent)
    {
        if (!timeEvent.ContainsKey(exec.globalTick))
        {
            timeEvent.Add(exec.globalTick, new List<TimelineEvent>());
        }
        timeEvent[exec.globalTick].Add(timelineEvent);
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent, int delay)
    {
        if (!timeEvent.ContainsKey(exec.globalTick + delay))
        {
            timeEvent.Add(exec.globalTick + delay, new List<TimelineEvent>());
        }
        timeEvent[exec.globalTick + delay].Add(timelineEvent);
    }

    public void Output()
    {
        foreach (var pair in timeEvent)
        {
            Debug.Log("tick " + pair.Key);

            foreach (TimelineEvent i in pair.Value)
            {
                float[] temp = i.GetData();
                string x = "";
                foreach (int xx in temp)
                {
                    x += xx;
                }
                Debug.Log(x);
            }
        }
    }
}
