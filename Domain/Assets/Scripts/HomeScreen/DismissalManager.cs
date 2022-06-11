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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        iconPool.GenerateSortRefresh(homeScreen, collectionHandler, gridParent,
            BaseUnitIcon.IconSetting.dismissal);
    }

    public void OnDisable()
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
