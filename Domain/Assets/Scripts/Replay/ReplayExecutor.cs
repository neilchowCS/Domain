using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager replayManager;
    public Tiler tiler;
    public List<GameObject> tiles;

    public Timeline timeline;
    public List<ReplayObject> replayObjects;
    public List<ReplayUnit> replayUnits;
    public List<ReplayProfile> profiles;
    public int index;
    public float timer;
    private bool inSpawnAnim;
    private float spawnAnimDuration = 1.5f;
    //public bool replayRun = false;

    void Awake()
    {
        tiles = tiler.CreateTiles();
        this.enabled = false;
    }

    // Update is called once per frame

    void Update()
    {
        timer += Time.deltaTime;
        if (inSpawnAnim)
        {
            if (timer >= spawnAnimDuration)
            {
                timer -= spawnAnimDuration;
                inSpawnAnim = false;
            }
        }else if (timer >= TickSpeed.secondsPerTick)
        {
            Advance();
            timer -= TickSpeed.secondsPerTick;
        }
    }

    public void StartReplay(Timeline t)
    {
        ClearRemnants();
        InitializeState(t);

        StartSpawn();
    }

    public void StartSpawn()
    {
        foreach (TimelineSpawn spawnEvent in timeline.initialSpawnEvents)
        {
            spawnEvent.ExecuteEvent(this);
        }
        inSpawnAnim = true;
    }

    public void Advance()
    {
        if (timeline.timeEvents.ContainsKey(index))
        {
            for (int i = 0; i < timeline.timeEvents[index].Count; i++)
            {
                timeline.timeEvents[index][i].ExecuteEvent(this);
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
