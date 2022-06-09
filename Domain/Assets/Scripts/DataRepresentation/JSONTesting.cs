using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONTesting : MonoBehaviour
{
    public UnitDataScriptableObject UDListSO;

    // Start is called before the first frame update
    public void GenerateJSON()
    {
        DataSerialization serializer = new DataSerialization();
        PlayerCollectionData playerCollectionData = new PlayerCollectionData();
        playerCollectionData.individualDataList.Add(new UnitIndividualData(0, 0));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(1, 1));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(2, 2));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(1, 3));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(0, 0));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(2, 0));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(1, 100));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(3, 2));
        playerCollectionData.individualDataList.Add(new UnitIndividualData(3, 20));
        string jsonOutput = serializer.SerializeData(playerCollectionData);
        Debug.Log(jsonOutput);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json", jsonOutput);


        //UnitIndividualCollection newCollection = serializer.DeserializeCollection(
        //    System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));

        StageDataCollection stageCollection = new StageDataCollection();

        StageDataCollection stageData = new StageDataCollection();
        PrimitiveTeamData stage0 = new PrimitiveTeamData();
        stage0.dataList.Add(new UnitIndividualData(1, 0));
        stage0.dataList.Add(new UnitIndividualData(1, 0));
        stage0.dataList.Add(new UnitIndividualData());
        stage0.dataList.Add(new UnitIndividualData(2, 0));
        stage0.positionList.Add(30);
        stage0.positionList.Add(31);
        stage0.positionList.Add(42);
        stage0.positionList.Add(43);
        stageData.stageDataList.Add(stage0);
        PrimitiveTeamData stage1 = new PrimitiveTeamData();
        stage1.dataList.Add(new UnitIndividualData());
        stage1.dataList.Add(new UnitIndividualData());
        stage1.dataList.Add(new UnitIndividualData());
        stage1.dataList.Add(new UnitIndividualData());
        stage1.positionList.Add(30);
        stage1.positionList.Add(31);
        stage1.positionList.Add(32);
        stage1.positionList.Add(33);
        stageData.stageDataList.Add(stage1);
        PrimitiveTeamData stage2 = new PrimitiveTeamData();
        stage2.dataList.Add(new UnitIndividualData(1, 0));
        stage2.dataList.Add(new UnitIndividualData(1, 0));
        stage2.dataList.Add(new UnitIndividualData(1, 0));
        stage2.dataList.Add(new UnitIndividualData(1, 0));
        stage2.positionList.Add(30);
        stage2.positionList.Add(31);
        stage2.positionList.Add(32);
        stage2.positionList.Add(33);
        stageData.stageDataList.Add(stage2);
        PrimitiveTeamData stage3 = new PrimitiveTeamData();
        stage3.dataList.Add(new UnitIndividualData(2, 0));
        stage3.dataList.Add(new UnitIndividualData(2, 0));
        stage3.dataList.Add(new UnitIndividualData(2, 0));
        stage3.dataList.Add(new UnitIndividualData(2, 0));
        stage3.positionList.Add(30);
        stage3.positionList.Add(31);
        stage3.positionList.Add(32);
        stage3.positionList.Add(33);
        stageData.stageDataList.Add(stage3);

        PrimitiveTeamData stage4 = new PrimitiveTeamData();
        stage4.dataList.Add(new UnitIndividualData(2, 0));
        stage4.positionList.Add(30);
        stageData.stageDataList.Add(stage4);

        PrimitiveTeamData stage5 = new PrimitiveTeamData();
        stage5.dataList.Add(new UnitIndividualData(3, 0));
        stage5.dataList.Add(new UnitIndividualData(3, 0));
        stage5.dataList.Add(new UnitIndividualData(3, 0));
        stage5.dataList.Add(new UnitIndividualData(3, 0));
        stage5.positionList.Add(30);
        stage5.positionList.Add(31);
        stage5.positionList.Add(32);
        stage5.positionList.Add(33);
        stageData.stageDataList.Add(stage5);

        jsonOutput = serializer.SerializeData(stageData);
        Debug.Log(jsonOutput);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/StageData.json", jsonOutput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
