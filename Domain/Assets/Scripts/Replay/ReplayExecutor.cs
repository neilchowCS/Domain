using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReplayExecutor : MonoBehaviour
{
    public ReplayManager replayManager;
    public Tiler tiler;
    public List<GameObject> tiles;

    public Timeline timeline;
    public List<ReplayObject> replayObjects;
    public List<ReplayUnit> replayUnits;
    public List<ReplayProfile> profiles;
    public List<ReplayProfile> side0Profiles;
    public List<ReplayProfile> side1Profiles;
    public List<Vector3> side0ProfilePositions;
    public List<Vector3> side1ProfilePositions;
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
        side0Profiles = new List<ReplayProfile>();
        side1Profiles = new List<ReplayProfile>();
        side0ProfilePositions = new List<Vector3>();
        side1ProfilePositions = new List<Vector3>();

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

    public void SetProfilePosition(ReplayProfile replayProfile)
    {
        if (replayProfile.transform.localPosition.x < 0)
        {
            int pos = side0Profiles.IndexOf(replayProfile);
            replayProfile.transform.position = side0ProfilePositions[pos];
        }
        else
        {
            int pos = side1Profiles.IndexOf(replayProfile);
            replayProfile.transform.position = side1ProfilePositions[pos];
        }
    }

    public void SetBars(ReplayProfile replayProfile, int side0Sum, int side1Sum)
    {
        if (replayProfile.transform.localPosition.x < 0)
        {
            replayProfile.bar.fillAmount = replayProfile.damageInt / (float)side0Sum;
        }
        else
        {
            replayProfile.bar.fillAmount = replayProfile.damageInt / (float)side1Sum;
        }
    }

    public void InitProfile(int globalSpawnId, UnitIndependentData indData, int side)
    {
        ReplayProfile y = GameObject.Instantiate(replayManager.profile).GetComponent<ReplayProfile>();
        profiles.Add(y);
        y.globalId = globalSpawnId;
        y.SetName(indData.baseData.unitName);
        y.SetImage(indData.baseData.unitSprite);
        y.transform.SetParent(replayManager.profileParent.transform, false);
        if (side == 0)
        {
            side0Profiles.Add(y);
            y.transform.localPosition = new Vector3(y.transform.localPosition.x,
                y.transform.localPosition.y - (side0ProfilePositions.Count) * 75,
                y.transform.localPosition.z);
            side0ProfilePositions.Add(y.transform.position);
        }
        else
        {
            side1Profiles.Add(y);
            y.transform.localPosition = new Vector3(-y.transform.localPosition.x,
                y.transform.localPosition.y - (side1ProfilePositions.Count) * 75,
                y.transform.localPosition.z);
            side1ProfilePositions.Add(y.transform.position);
        }
    }

    public void ReorderProfile()
    {
        side0Profiles = side0Profiles.OrderByDescending(o => o.damageInt).ToList();
        side1Profiles = side1Profiles.OrderByDescending(o => o.damageInt).ToList();
        int side0DamageSum = side0Profiles[0].damageInt;
        int side1DamageSum = side1Profiles[0].damageInt;

        foreach (ReplayProfile i in profiles)
        {
            SetProfilePosition(i);
            SetBars(i, side0DamageSum, side1DamageSum);
        }
    }

}
