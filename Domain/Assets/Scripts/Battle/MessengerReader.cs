using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerReader : MonoBehaviour
{
    public BattleRecord record;
    public bool hasRead = false;
    public bool replayFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Reads team messenger and sets team0, stage id. 
    /// Returns false if no messenger found
    /// </summary>
    public bool ReadTeamMessenger()
    {
        if (!hasRead)
        {
            hasRead = true;
            TeamMessenger m = FindObjectOfType<TeamMessenger>();
            if (m)
            {
                replayFlag = false;
                record = m.teamRecord;

                StoreReplay();

                foreach (TeamMessenger messenger in FindObjectsOfType<TeamMessenger>())
                {
                    Destroy(messenger.gameObject);
                }
                foreach (ReplayMessenger messenger in FindObjectsOfType<ReplayMessenger>())
                {
                    Destroy(messenger.gameObject);
                }
                return true;
            }

            ReplayMessenger r = FindObjectOfType<ReplayMessenger>();
            if (r)
            {
                replayFlag = true;
                record = new BattleRecord(r.record);
                UnityEngine.Random.state = r.record.seed;

                foreach (TeamMessenger messenger in FindObjectsOfType<TeamMessenger>())
                {
                    Destroy(messenger.gameObject);
                }
                foreach (ReplayMessenger messenger in FindObjectsOfType<ReplayMessenger>())
                {
                    Destroy(messenger.gameObject);
                }
                return true;
            }
            record = new();
        }
        return false;
    }

    public void StoreReplay()
    {
        ReplayStorage storage = DataSerialization.DeserializeReplayStore(
                System.IO.File.ReadAllText(Application.persistentDataPath + "/ReplayRecord.json"));
        if (storage == null)
        {
            storage = new ReplayStorage();
        }
        else if (storage.replayRecords.Count >= 20)
        {
            storage.replayRecords.RemoveAt(storage.replayRecords.Count - 1);
        }

        storage.replayRecords.Insert(0, new ReplayRecord(record, UnityEngine.Random.state));

        string jsonOutput = DataSerialization.SerializeData(storage);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ReplayRecord.json", jsonOutput);
    }
}
