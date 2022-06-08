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
    public enum SortState { level, unitId }
    public SortState sortState;

    private void Awake()
    {
        unitButtonList = new List<OpenUnitProfile>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeCollection();
    }

    public void InitializeCollection()
    {
        DataSerialization serializer = new DataSerialization();
        collection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
        if (unitButtonList.Count == 0)
        {
            foreach (UnitIndividualData data in collection.individualDataList)
            {
                OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                //FIXME should be manager not homescreen
                x.homeScreen = this.homeScreen;
                x.InitButton(UDListScriptableObject.uDList[data.unitId].unitSprite,
                    data.level);
                unitButtonList.Add(x);
            }
        }
        else
        {
            for (int i = 0; i < collection.individualDataList.Count - unitButtonList.Count; i++)
            {
                OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                x.homeScreen = this.homeScreen;
                unitButtonList.Add(x);
            }
        }

        SortCollection(SortState.level);
    }

    public void SortCollection()
    {
        if (sortState == SortState.unitId)
        {
            SortCollection(SortState.level);
        }else if (sortState == SortState.level)
        {
            SortCollection(SortState.unitId);
        }
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

            DataSerialization serializer = new DataSerialization();
            System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerCollection.json",
                serializer.SerializeData(collection));
            for (int i = 0; i < unitButtonList.Count; i++)
            {
                unitButtonList[i].InitButton(UDListScriptableObject.uDList[collection.individualDataList[i].unitId].unitSprite,
                    collection.individualDataList[i].level);
            }
        
    }
}
