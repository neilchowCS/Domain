using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPool : MonoBehaviour
{
    public List<BaseUnitIcon> iconList;

    public BaseUnitIcon unitProfileButtonPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        iconList = new List<BaseUnitIcon>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public BaseUnitIcon NewUnitIcon()
    {
        BaseUnitIcon x = Instantiate(unitProfileButtonPrefab);
        iconList.Add(x);
        return x;
    }

    public BaseUnitIcon NewUnitIcon(GameObject parent)
    {
        BaseUnitIcon x = Instantiate(unitProfileButtonPrefab, parent.transform);
        iconList.Add(x);
        return x;
    }

    public void CreateIcons(PlayerCollectionData collection, GameObject parent)
    {
        if (iconList.Count == 0)
        {
            foreach (UnitIndividualData data in collection.individualDataList)
            {
                NewUnitIcon(parent);
            }
        }
        else
        {
            int difference = collection.individualDataList.Count - iconList.Count;
            for (int i = 0; i < difference; i++)
            {
                NewUnitIcon(parent);
            }
        }
    }

    public void RefreshIcons(HomeScreen homeScreen,
        CollectionHandler collectionHandler, BaseUnitIcon.IconSetting iconSetting)
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            iconList[i].InitButton(homeScreen, iconSetting,

                collectionHandler.uDListSO.uDList[
                    collectionHandler.collection.individualDataList[i].unitId].unitSprite,

                collectionHandler.collection.individualDataList[i].level,

                ElementColor.GetColor(collectionHandler.uDListSO.uDList[
                    collectionHandler.collection.individualDataList[i].unitId].elementEnum));
        }
    }

    public void GenerateSortRefresh(HomeScreen homeScreen, CollectionHandler collectionHandler,
        GameObject parent, BaseUnitIcon.IconSetting iconSetting)
    {
        CreateIcons(collectionHandler.collection, parent);
        collectionHandler.SortCollection(CollectionHandler.SortState.level);
        RefreshIcons(homeScreen, collectionHandler, iconSetting);
    }
}
