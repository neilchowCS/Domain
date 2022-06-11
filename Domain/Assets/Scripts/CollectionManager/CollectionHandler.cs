using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionHandler : MonoBehaviour
{
    public PlayerCollectionData collection;
    public UDListScriptableObject uDListSO;

    public enum SortState { level, unitId }
    public SortState sortState;

    private void Awake()
    {
        collection = DataSerialization.DeserializeStaticCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadFromJSON()
    {
        collection = DataSerialization.DeserializeStaticCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
    }

    public PlayerCollectionData GetNewCollection()
    {
        ReadFromJSON();
        return collection;
    }

    public void WriteCollection()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json",
                DataSerialization.SerializeStaticCollection(collection));
    }

    public void SortCollection(SortState state)
    {
        switch (state)
        {
            case SortState.level:
                collection.individualDataList =
                    collection.individualDataList.OrderByDescending(o => o.level).
                        ThenBy(o => o.unitId).ToList();
                sortState = SortState.level;
                break;
            case SortState.unitId:
                collection.individualDataList =
                    collection.individualDataList.OrderBy(o => o.unitId).
                        ThenByDescending(o => o.level).ToList();
                sortState = SortState.unitId;
                break;
        }

        WriteCollection();
    }
}
