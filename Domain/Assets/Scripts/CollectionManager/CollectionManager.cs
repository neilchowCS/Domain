using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionManager : MonoBehaviour
{
    public HomeScreen homeScreen;
    public GameObject gridParent;
    public List<BaseUnitIcon> unitButtonList;
    public BaseUnitIcon unitProfileButtonPrefab;
    public UDListScriptableObject UDListScriptableObject;

    public CollectionHandler collectionHandler;

    private void Awake()
    {
        unitButtonList = new List<BaseUnitIcon>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeCollection();
    }

    public void InitializeCollection()
    {
        Debug.Log(collectionHandler.collection.individualDataList.Count);
        Debug.Log(unitButtonList.Count);
        if (unitButtonList.Count == 0)
        {
            foreach (UnitIndividualData data in collectionHandler.collection.individualDataList)
            {
                BaseUnitIcon x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                //FIXME should be manager not homescreen
                x.InitButton(homeScreen, BaseUnitIcon.IconSetting.collection,
                    UDListScriptableObject.uDList[data.unitId].unitSprite, data.level,
                    ElementColor.GetColor(UDListScriptableObject.uDList[data.unitId].elementEnum));
                unitButtonList.Add(x);
            }
        }
        else
        {
            int difference = collectionHandler.collection.individualDataList.Count - unitButtonList.Count;
            for (int i = 0; i < difference; i++)
            {
                BaseUnitIcon x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
                unitButtonList.Add(x);
            }
        }

        collectionHandler.SortCollection(CollectionHandler.SortState.level);
        RefreshButton();
    }

    public void SortButton()
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
            unitButtonList[i].InitButton(
                homeScreen, BaseUnitIcon.IconSetting.collection,

                UDListScriptableObject.uDList
                [collectionHandler.collection.individualDataList[i].unitId].unitSprite,

                collectionHandler.collection.individualDataList[i].level,

                ElementColor.GetColor(UDListScriptableObject.uDList
                [collectionHandler.collection.individualDataList[i].unitId].elementEnum));
        }

    }
}
