using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline
{
    public BattleExecutor exec;

    public List<TimelineSpawn> initialSpawnEvents;
    public SortedDictionary<int, List<TimelineEvent>> timeEvents;

    public Timeline(BattleExecutor e)
    {
        exec = e;
        initialSpawnEvents = new List<TimelineSpawn>();
        timeEvents = new SortedDictionary<int, List<TimelineEvent>>();
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent)
    {
        if (!timeEvents.ContainsKey(exec.globalTick))
        {
            timeEvents.Add(exec.globalTick, new List<TimelineEvent>());
        }
        timeEvents[exec.globalTick].Add(timelineEvent);
    }

    public void AddInitialSpawn(TimelineSpawn timelineEvent)
    {
        initialSpawnEvents.Add(timelineEvent);
    }

    public void AddTimelineEvent(TimelineEvent timelineEvent, int delay)
    {
        if (!timeEvents.ContainsKey(exec.globalTick + delay))
        {
            timeEvents.Add(exec.globalTick + delay, new List<TimelineEvent>());
        }
        timeEvents[exec.globalTick + delay].Add(timelineEvent);
    }

    public void Output()
    {
        foreach (var pair in timeEvents)
        {
            foreach (TimelineEvent i in pair.Value)
            {
                float[] temp = i.GetData();
                string x = "";
                foreach (int xx in temp)
                {
                    x += xx;
                }
            }
        }
    }
}
