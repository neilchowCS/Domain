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
        PlayerCollection collection = new PlayerCollection();
        collection.collectionList.Add(new UnitIndividualData(0, 0));
        collection.collectionList.Add(new UnitIndividualData(1, 0));
        collection.collectionList.Add(new UnitIndividualData(2, 0));
        collection.collectionList.Add(new UnitIndividualData(1, 0));
        string jsonOutput = serializer.SerializeCollection(collection);
        Debug.Log(jsonOutput);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json", jsonOutput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
