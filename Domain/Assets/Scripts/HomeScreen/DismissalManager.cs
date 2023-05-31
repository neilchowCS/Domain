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

    public List<IconGeneric> selectedButtonList;
    public int dismissalCap = 5;

    public TextMeshProUGUI dismissPreview;

    void OnEnable()
    {
        iconPool.GenerateSortRefresh(homeScreen, collectionHandler, gridParent,
            IconGeneric.IconSetting.dismissal);
    }

    void OnDisable()
    {
        ClearMenus();
    }

    public void ChosenUnit(IconGeneric dismissalIcon)
    {
        if (selectedButtonList.Count < dismissalCap)
        {
            dismissalIcon.transform.SetParent(selectedParent.transform);
            selectedButtonList.Add(dismissalIcon);
            dismissalIcon.iconSetting = IconGeneric.IconSetting.dismissalSelect;
            int output = 0;
            foreach (IconGeneric selected in selectedButtonList)
            {
                output += LevelCost.GetSalePrice(selected.individualData.level);
            }
            dismissPreview.text = $"Output: {output} essence";
        }
    }

    public void ExecuteDismissal()
    {

        foreach (IconGeneric selected in selectedButtonList)
        {
            collectionHandler.collection.individualDataList.Remove(selected.individualData);

            resourceHandler.playerData.essence +=
                LevelCost.GetSalePrice(selected.individualData.level);
        }
        collectionHandler.WriteCollection();
        resourceHandler.WritePlayerData();
        iconPool.GenerateRefresh(homeScreen, collectionHandler,
            gridParent, IconGeneric.IconSetting.dismissal);

        ClearMenus();
    }

    private void ClearMenus()
    {
        dismissPreview.text = "";
        selectedButtonList = new List<IconGeneric>();
    }
}
