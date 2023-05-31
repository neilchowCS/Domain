using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPool : MonoBehaviour
{
    public List<IconGeneric> iconList;

    public IconGeneric unitProfileButtonPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        iconList = new List<IconGeneric>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //GENERATE DIFFERENCE

    public IconGeneric NewUnitIcon()
    {
        IconGeneric x = Instantiate(unitProfileButtonPrefab);
        iconList.Add(x);
        return x;
    }

    public IconGeneric NewUnitIcon(GameObject parent)
    {
        IconGeneric x = Instantiate(unitProfileButtonPrefab, parent.transform);
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

        int difference = collection.individualDataList.Count - iconList.Count;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                NewUnitIcon(parent);
            }
        }
        else
        {
            while (iconList.Count > collection.individualDataList.Count)
            {
                //Debug.Log($"icon {iconList.Count}, data {collection.individualDataList.Count}");
                IconGeneric i = iconList[^1];
                iconList.Remove(i);
                Destroy(i.gameObject);
            }
        }
    }

    public void RefreshIcons(HomeScreen homeScreen,
        CollectionHandler collectionHandler, IconGeneric.IconSetting iconSetting)
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            iconList[i].InitIcon(homeScreen, iconSetting, collectionHandler.uDListSO,
                collectionHandler.collection.individualDataList[i]);
        }
    }

    public void GenerateSortRefresh(HomeScreen homeScreen, CollectionHandler collectionHandler,
        GameObject parent, IconGeneric.IconSetting iconSetting)
    {
        foreach (IconGeneric icon in iconList)
        {
            icon.transform.SetParent(null);
            icon.transform.SetParent(parent.transform);
        }

        CreateIcons(collectionHandler.collection, parent);
        collectionHandler.SortCollection(CollectionHandler.SortState.level);
        RefreshIcons(homeScreen, collectionHandler, iconSetting);
    }

    public void GenerateRefresh(HomeScreen homeScreen, CollectionHandler collectionHandler,
        GameObject parent, IconGeneric.IconSetting iconSetting)
    {
        CreateIcons(collectionHandler.collection, parent);
        RefreshIcons(homeScreen, collectionHandler, iconSetting);
    }
}