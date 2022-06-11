using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

//Banish unit?
public class DismissalManager : MonoBehaviour
{
    public HomeScreen homeScreen;

    public PlayerData playerData;
    public CollectionHandler collectionHandler;
    public IconPool iconPool;

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
        foreach (BaseUnitIcon icon in selectedButtonList)
        {
            icon.transform.SetParent(gridParent.transform);
        }
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
        DataSerialization serializer = new DataSerialization();
        playerData = serializer.DeserializePlayerData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));
        playerData.essence += selectedButtonList.Count * 10;
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json",
            serializer.SerializeData(playerData));

        foreach (BaseUnitIcon selected in selectedButtonList)
        {
            collectionHandler.collection.individualDataList.Remove(selected.individualData);
            //playerData.essence += 10;
        }
        collectionHandler.WriteCollection();
        iconPool.GenerateRefresh(homeScreen, collectionHandler,
            gridParent, BaseUnitIcon.IconSetting.dismissal);

        dismissPreview.text = "";
        selectedButtonList = new List<BaseUnitIcon>();
    }
}
