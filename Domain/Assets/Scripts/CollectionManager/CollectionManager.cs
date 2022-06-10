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
    public CollectionHandler collectionHandler;

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
        //Debug.Log(collection.individualDataList.Count);
        //Debug.Log(unitButtonList.Count);
        if (unitButtonList.Count == 0)
        {
            foreach (UnitIndividualData data in collectionHandler.collection.individualDataList)
            {
                OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                //FIXME should be manager not homescreen
                x.homeScreen = this.homeScreen;
                x.InitButton(UDListScriptableObject.uDList[data.unitId].unitSprite, data.level,
                    ElementColor.GetColor(UDListScriptableObject.uDList[data.unitId].elementEnum));
                unitButtonList.Add(x);
            }
        }
        else
        {
            int difference = collectionHandler.collection.individualDataList.Count - unitButtonList.Count;
            for (int i = 0; i < difference; i++)
            {
                OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                x.homeScreen = this.homeScreen;
                unitButtonList.Add(x);
            }
        }

        collectionHandler.SortCollection(CollectionHandler.SortState.level);
        RefreshButton();
    }

    public void SortCollection()
    {
        if (collectionHandler.sortState == CollectionHandler.SortState.unitId)
        {
            collectionHandler.SortCollection(CollectionHandler.SortState.level);
        }else if (collectionHandler.sortState == CollectionHandler.SortState.level)
        {
            collectionHandler.SortCollection(CollectionHandler.SortState.unitId);
        }

        RefreshButton();
    }

    public void RefreshButton()
    {

        for (int i = 0; i < unitButtonList.Count; i++)
        {
            unitButtonList[i].InitButton(UDListScriptableObject.uDList
                [collectionHandler.collection.individualDataList[i].unitId].unitSprite,

                collectionHandler.collection.individualDataList[i].level,

                ElementColor.GetColor(UDListScriptableObject.uDList
                [collectionHandler.collection.individualDataList[i].unitId].elementEnum));
        }

    }
}
