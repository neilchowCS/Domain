using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONTesting : MonoBehaviour
{
    public UnitDataScriptableObject UDListSO;

    // Start is called before the first frame update
    void Start()
    {
        DataSerialization serializer = new DataSerialization();
        UnitIndividualCollection collection = new UnitIndividualCollection();
        collection.collection.Add(new UnitIndividualData(0, 0));
        collection.collection.Add(new UnitIndividualData(1, 0));
        collection.collection.Add(new UnitIndividualData(2, 0));
        collection.collection.Add(new UnitIndividualData(1, 0));
        string jsonOutput = serializer.SerializeData(collection);
        Debug.Log(jsonOutput);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json", jsonOutput);


        UnitIndividualCollection newCollection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));

        StageDataCollection stageCollection = new StageDataCollection();

        StageDataCollection stageData = new StageDataCollection();
        PrimitiveTeamData stage0 = new PrimitiveTeamData();
        stage0.dataList.Add(new UnitIndividualData());
        stage0.dataList.Add(new UnitIndividualData());
        stage0.dataList.Add(new UnitIndividualData());
        stage0.dataList.Add(new UnitIndividualData());
        stage0.positionList.Add(0);
        stage0.positionList.Add(1);
        stage0.positionList.Add(2);
        stage0.positionList.Add(3);
        stageData.stageDataList.Add(stage0);

        jsonOutput = serializer.SerializeData(stageData);
        Debug.Log(jsonOutput);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/StageData.json", jsonOutput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
