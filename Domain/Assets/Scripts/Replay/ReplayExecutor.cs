using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager rm;
    public Timeline timeline;
    private List<GameObject> runtime;

    // Start is called before the first frame update
    void Start()
    {
        runtime = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartReplay(Timeline t)
    {
        foreach (GameObject x in runtime)
        {
            Destroy(x);
        }

        timeline = t;

        Debug.Log(timeline.timeline[0].Count);

        for (int i = 0; i < timeline.timeline[0].Count; i++)
        {
            Debug.Log(timeline.timeline[0][i].eventId);
            if (timeline.timeline[0][i].eventId == 1)
            {
                float[] tEvent = timeline.timeline[0][i].GetData();
                GameObject x = Instantiate(rm.prefabs[(int)tEvent[0]]);
                Debug.Log(x.name);
                x.transform.position = new Vector3(tEvent[4], tEvent[5] + .5f , tEvent[6]);
                if (tEvent[3] == 1)
                {
                    x.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                runtime.Add(x);
            }
        }
    }
}
