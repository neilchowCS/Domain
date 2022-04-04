using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline
{
    public BattleExecutor exec;

    public SortedDictionary<int, List<TimelineEvent>> timeline;

    public Timeline(BattleExecutor e)
    {
        exec = e;
        timeline = new SortedDictionary<int, List<TimelineEvent>>();
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent)
    {
        if (!timeline.ContainsKey(exec.globalTick))
        {
            timeline.Add(exec.globalTick, new List<TimelineEvent>());
        }
        timeline[exec.globalTick].Add(timelineEvent);
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent, int delay)
    {
        if (!timeline.ContainsKey(exec.globalTick + delay))
        {
            timeline.Add(exec.globalTick + delay, new List<TimelineEvent>());
        }
        timeline[exec.globalTick + delay].Add(timelineEvent);
    }

    public void Output()
    {
        foreach (var pair in timeline)
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
