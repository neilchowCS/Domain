using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager replayManager;
    public Timeline timeline;
    public List<ReplayObject> replayObjects;
    public List<ReplayUnit> replayUnits;
    public List<ReplayProfile> profiles;
    public int index;
    public float timer;
    public float secondsPerTick;
    //public bool replayRun = false;

    void Awake()
    {
        this.enabled = false;
        secondsPerTick = 1f / TickSpeed.ticksPerSecond;
    }

    // Update is called once per frame

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= secondsPerTick)
        {
            Advance();
            timer -= secondsPerTick;
        }
    }

    public void StartReplay(Timeline t)
    {
        ClearRemnants();
        InitializeState(t);

        Advance();
    }

    public void Advance()
    {
        if (timeline.timeEvent.ContainsKey(index))
        {
            for (int i = 0; i < timeline.timeEvent[index].Count; i++)
            {
                timeline.timeEvent[index][i].ExecuteEvent(this);
            }
        }
        index++;
    }

    public void ClearRemnants()
    {
        if (replayObjects != null)
        {
            foreach (ReplayObject x in replayObjects)
            {
                Destroy(x.gameObject);
            }
        }
        if (profiles != null)
        {
            foreach (ReplayProfile x in profiles)
            {
                Destroy(x.gameObject);

            }

        }
    }

    public void InitializeState(Timeline t)
    {
        this.enabled = true;

        replayObjects = new List<ReplayObject>();
        replayUnits = new List<ReplayUnit>();
        profiles = new List<ReplayProfile>();

        timeline = t;
        index = 0;
        timer = 0;
    }

    public int GetNumberOfSide(int side)
    {
        int count = 0;
        foreach (ReplayUnit unit in replayUnits)
        {
            if (unit.side == side)
            {
                count++;
            }
        }
        return count;
    }

}
