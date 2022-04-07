using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager rm;
    public Timeline timeline;
    public List<ReplayObject> replayObjects;
    public List<ReplayUnit> replayUnits;
    public List<ReplayProfile> profiles;
    public int index;
    public float timer;
    public bool replayRun = false;

    public int side0 = 0;
    public int side1 = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    
    void Update()
    {
        if (replayRun)
        {
            timer += Time.deltaTime;
            if (timer >= .5f)
            {
                Advance();
                timer = 0;
            }
        }
    }

    public void StartReplay(Timeline t)
    {
        this.enabled = true;

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
        side0 = 0;
        side1 = 0;

        replayObjects = new List<ReplayObject>();
        replayUnits = new List<ReplayUnit>();
        profiles = new List<ReplayProfile>();

        timeline = t;
        index = 0;
        timer = 0;
        replayRun = true;

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
}
