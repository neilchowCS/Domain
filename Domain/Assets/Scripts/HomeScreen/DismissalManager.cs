using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

//Banish unit?
public class DismissalManager : MonoBehaviour
{
    public HomeScreen homeScreen;
    
    public CollectionHandler collectionHandler;
    public IconPool iconPool;
    public ResourceHandler resourceHandler;

    public GameObject gridParent;
    public GameObject selectedParent;

    public List<BaseUnitIcon> selectedButtonList;
    public int dismissalCap = 5;

    public TextMeshProUGUI dismissPreview;

    void OnEnable()
    {
        iconPool.GenerateSortRefresh(homeScreen, collectionHandler, gridParent,
            BaseUnitIcon.IconSetting.dismissal);
    }

    void OnDisable()
    {
        ClearMenus();
    }

    public void ChosenUnit(BaseUnitIcon dismissalIcon)
    {
        if (selectedButtonList.Count < dismissalCap)
        {
            dismissalIcon.transform.SetParent(selectedParent.transform);
            selectedButtonList.Add(dismissalIcon);
            dismissPreview.text = $"Output: {selectedButtonList.Count * 10} essence";
        }
    }

    public void ExecuteDismissal()
    {
        resourceHandler.playerData.essence += 100;
        resourceHandler.WritePlayerData();

        foreach (BaseUnitIcon selected in selectedButtonList)
        {
            collectionHandler.collection.individualDataList.Remove(selected.individualData);
            //playerData.essence += 10;
        }
        collectionHandler.WriteCollection();
        iconPool.GenerateRefresh(homeScreen, collectionHandler,
            gridParent, BaseUnitIcon.IconSetting.dismissal);

        ClearMenus();
    }

    private void ClearMenus()
    {
        dismissPreview.text = "";
        selectedButtonList = new List<BaseUnitIcon>();
    }
}
