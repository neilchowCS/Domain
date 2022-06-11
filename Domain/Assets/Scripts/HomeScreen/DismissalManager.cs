using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        dismissalIcon.transform.SetParent(selectedParent.transform);
        selectedButtonList.Add(dismissalIcon);
    }
}
