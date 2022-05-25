using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageNumberTesting : MonoBehaviour
{
    public TextMeshProUGUI stageIndicator;
    public int stage;
    private int min;
    private int max;
    // Start is called before the first frame update
    void Start()
    {
        DataSerialization serializer = new DataSerialization();
        StageDataCollection stageList = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));
        stage = 0;
        min = 0;
        max = stageList.stageDataList.Count - 1;
        stageIndicator.text = stage + "";
    }

    public void IncrementStage()
    {
        if (stage < max)
        {
            stage++;
        }
        stageIndicator.text = stage + "";
    }

    public void DecrementStage()
    {
        if (stage > min)
        {
            stage--;
        }
        stageIndicator.text = stage + "";
    }
}
