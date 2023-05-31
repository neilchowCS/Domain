using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectionManager : MonoBehaviour
{
    public HomeScreen homeScreen;
    public GameObject gridParent;

    public CollectionHandler collectionHandler;
    public IconPool iconPool;


    void OnEnable()
    {
        iconPool.GenerateSortRefresh(homeScreen, collectionHandler, gridParent,
            IconGeneric.IconSetting.collection);
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

        iconPool.RefreshIcons(homeScreen, collectionHandler, IconGeneric.IconSetting.collection);
    }
}
