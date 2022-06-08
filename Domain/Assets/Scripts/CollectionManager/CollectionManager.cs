using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionManager : MonoBehaviour
{
    public HomeScreen homeScreen;
    public GameObject gridParent;
    public List<OpenUnitProfile> unitButtonList;
    public OpenUnitProfile unitProfileButtonPrefab;
    public UDListScriptableObject UDListScriptableObject;

    public PlayerData playerData;
    public PlayerCollectionData collection;
    public enum SortState { unsorted, level, unitId }
    public SortState sortState = SortState.unsorted;

    // Start is called before the first frame update
    void Awake()
    {
        unitButtonList = new List<OpenUnitProfile>();
        InitializeCollection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCollection()
    {
        DataSerialization serializer = new DataSerialization();
        collection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
        foreach (UnitIndividualData data in collection.individualDataList)
        {
            OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
            //FIXME should be manager not homescreen
            x.InitButton(this.homeScreen, UDListScriptableObject.uDList[data.unitId].unitSprite);
            Debug.Log("here");
            unitButtonList.Add(x);
        }
    }

    public void SortCollection()
    {
        if (sortState == SortState.unsorted || sortState == SortState.unitId)
        {
            collection.individualDataList =
                collection.individualDataList.OrderByDescending(o => o.level).ToList();
            sortState = SortState.level;
        }else if (sortState == SortState.level)
        {
            collection.individualDataList =
                collection.individualDataList.OrderBy(o => o.unitId).ToList();
            sortState = SortState.unitId;
        }
        DataSerialization serializer = new DataSerialization();
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json",
            serializer.SerializeData(collection));
        for (int i = 0; i < unitButtonList.Count; i++)
        {
            Debug.Log("here");
            unitButtonList[i].image.sprite =
                UDListScriptableObject.uDList[collection.individualDataList[i].unitId].unitSprite;
        }
    }
}
