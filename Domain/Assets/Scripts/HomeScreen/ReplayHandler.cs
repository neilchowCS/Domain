using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayHandler : MonoBehaviour
{
    public CollectionHandler collectionHandler;
    public IconGeneric prefabIcon;
    public ReplayInstanceUI prefabReplayInstance;
    public GameObject scrollContents;

    // Start is called before the first frame update
    void Start()
    {
        ReplayStorage storage = DataSerialization.DeserializeReplayStore(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/ReplayRecord.json"));

        /*
         * Top left: -114.5, 65
            lower: negative y
            side: 0.5

            Delta x = 115
        */
        float deltaX = 115;
        if (storage != null)
        {
            for (int i = 0; i < storage.replayRecords.Count; i++)
            {
                ReplayInstanceUI ui = Instantiate(prefabReplayInstance, scrollContents.transform);
                ui.replayRecord = storage.replayRecords[i];

                Vector3 position = new Vector3(-114.5f, 65, 0);
                for (int j = 0; j < storage.replayRecords[i].team0Data.Count; j++)
                {
                    IconGeneric icon = Instantiate(prefabIcon, ui.p1GridLayout.transform);
                    icon.transform.localPosition = position;
                    icon.transform.localScale = new Vector3(0.527f, 0.527f, 0.527f);

                    icon.RecastIcon(collectionHandler.uDListSO, storage.replayRecords[i].team0Data[j]);

                    position += new Vector3(deltaX, 0, 0);
                    if (j == 2)
                    {
                        position = new Vector3(position.x - deltaX * 3, -position.y, 0);
                    }
                }

                position = new Vector3(114.5f - deltaX * 2, 65, 0);
                for (int j = 0; j < storage.replayRecords[i].team1Data.Count; j++)
                {
                    IconGeneric icon = Instantiate(prefabIcon, ui.p2GridLayout.transform);
                    icon.transform.localPosition = position;
                    icon.transform.localScale = new Vector3(0.527f, 0.527f, 0.527f);

                    icon.RecastIcon(collectionHandler.uDListSO, storage.replayRecords[i].team1Data[j]);

                    position += new Vector3(deltaX, 0, 0);
                    if (j == 2)
                    {
                        position = new Vector3(position.x - deltaX * 3, -position.y, 0);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
