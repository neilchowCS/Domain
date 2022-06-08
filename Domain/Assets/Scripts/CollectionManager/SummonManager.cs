using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummonManager : MonoBehaviour
{
    public int numberOfUnits = 4;
    public TextMeshProUGUI summonDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformSummon()
    {
        DataSerialization serializer = new DataSerialization();
        PlayerCollectionData collection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
        int output = Random.Range(0, numberOfUnits);
        DisplaySummon(output);
        UnitIndividualData individualData = new UnitIndividualData(output, 1);
        collection.individualDataList.Add(individualData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json",
            serializer.SerializeData(collection));

    }

    public void DisplaySummon(int i)
    {
        summonDisplay.text = i + "";
    }
}
