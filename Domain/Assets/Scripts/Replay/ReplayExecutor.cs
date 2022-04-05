using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager rm;
    public Timeline timeline;
    private List<ReplayObject> replayObjects;
    private List<ReplayUnit> replayUnits;
    public int index;
    public float timer;
    private bool replayRun = false;

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
            if (timer >= .2f)
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

        replayObjects = new List<ReplayObject>();
        replayUnits = new List<ReplayUnit>();

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
            Debug.Log(timeline.timeEvent[index].Count);

            for (int i = 0; i < timeline.timeEvent[index].Count; i++)
            {
                Debug.Log(timeline.timeEvent[index][i].eventId);

                //spawn
                // eventId 1
                // 0 = spawnId
                // 1 = globalSpawnId
                // 2 = side
                // 3 = spawnTileId
                // 4 = spawnPosX
                // 5 = spawnPosY
                // 6 = spawnPosZ
                if (timeline.timeEvent[index][i].eventId == 1)
                {
                    float[] tEvent = timeline.timeEvent[index][i].GetData();
                    GameObject x = Instantiate(rm.prefabs[(int)tEvent[0]]);
                    //Debug.Log(x.name);
                    x.transform.position = new Vector3(tEvent[4], tEvent[5] + .5f, tEvent[6]);
                    if (tEvent[3] == 1)
                    {
                        x.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                    replayObjects.Add(x.GetComponent<ReplayUnit>());
                    replayUnits.Add(x.GetComponent<ReplayUnit>());
                    replayUnits[replayUnits.Count - 1].globalId = (int)tEvent[1];

                }
                //target
                // eventId 2
                // 0 = selfId
                // 1 = selfTarget
                else if (timeline.timeEvent[index][i].eventId == 2)
                {
                    float[] tEvent = timeline.timeEvent[index][i].GetData();
                    ReplayObject self = null;
                    ReplayObject target = null;
                    foreach (ReplayObject rO in replayObjects)
                    {
                        if (rO.globalId == tEvent[0])
                        {
                            self = rO;
                        }
                        else if (rO.globalId == tEvent[1])
                        {
                            target = rO;
                        }
                    }
                    if (self != null && target != null)
                    {
                        self.gameObject.GetComponent<ReplayUnit>().target = target;
                    }
                    else
                    {
                        Debug.Log("Change target failed");
                    }
                }
                //move
                // eventId 3
                // 0 = selfId
                // 1 = nextTileId
                // 2 = ntPosX
                // 3 = ntPosY
                // 4 = ntPosZ
                else if (timeline.timeEvent[index][i].eventId == 3)
                {
                    float[] tEvent = timeline.timeEvent[index][i].GetData();
                    ReplayObject self = null;
                    Vector3 destination = new Vector3(tEvent[2], tEvent[3] + .5f, tEvent[4]);
                    foreach (ReplayObject rO in replayObjects)
                    {
                        if (rO.globalId == tEvent[0])
                        {
                            self = rO;
                        }
                    }
                    if (self != null)
                    {
                        self.gameObject.GetComponent<ReplayUnit>().destination = destination;
                        self.gameObject.GetComponent<ReplayUnit>().moving = true;
                    }
                    else
                    {
                        Debug.Log("Movement failed");
                    }

                }
                //target
                // eventId 4
                // 0 = selfId
                else if (timeline.timeEvent[index][i].eventId == 4)
                {
                    float[] tEvent = timeline.timeEvent[index][i].GetData();
                    ReplayObject self = null;
                    foreach (ReplayObject rO in replayObjects)
                    {
                        if (rO.globalId == tEvent[0])
                        {
                            self = rO;
                        }
                    }
                    if (self != null)
                    {
                        replayObjects.Remove(self);
                        replayUnits.Remove(self.gameObject.GetComponent<ReplayUnit>());
                        Destroy(self.gameObject);
                    }
                }
                //END
                else if (timeline.timeEvent[index][i].eventId == -1)
                {
                    replayRun = false;
                    this.enabled = false;
                }
            }
        }

        index++;
    }
}
