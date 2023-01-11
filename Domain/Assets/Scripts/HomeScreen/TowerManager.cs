using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public HomeScreen home;
    public GameObject contentParent;
    public TowerEntrance entrancePrefab;
    public List<TowerEntrance> entrancePool;

    // Start is called before the first frame update
    void Start()
    {
        InitTower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitTower()
    {
        DataSerialization serializer = new();
        StageDataCollection stageList = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));
        if (entrancePool.Count == 0)
        {
            for (int i = 0; i < stageList.stageDataList.Count; i++)
            {
                TowerEntrance temp = Instantiate(entrancePrefab, contentParent.transform);
                entrancePool.Add(temp);
                temp.stageId = i;
                temp.stageNumText.text = $"Stage {i}";
                temp.parent = this;
            }
        }
    }
}
