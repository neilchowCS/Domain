using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconGeneric : MonoBehaviour
{
    public UnitIndividualData individualData;
    public Image image;
    public Image elementCircle;
    public TextMeshProUGUI levelText; //also item count display

    public int itemId;
    public int itemCount;

    public enum IconSetting { nil, inventory, collection, dismissal, dismissalSelect }
    public IconSetting iconSetting = IconSetting.nil;

    public HomeScreen homeScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitIcon(HomeScreen homeScreen, IconSetting setting,
        UDListScriptableObject UDListSO, UnitIndividualData individual)
    {
        this.homeScreen = homeScreen;
        iconSetting = setting;
        RecastIcon(UDListSO, individual);
    }

    public void InitIcon(HomeScreen homeScreen, IconSetting setting, int itemId, int itemCount,
        Sprite itemIcon)
    {
        this.homeScreen = homeScreen;
        iconSetting = setting;
        RecastIcon(itemId, itemCount, itemIcon);
    }

    public void RecastIcon(UDListScriptableObject UDListSO, UnitIndividualData individual)
    {
        individualData = individual;
        image.sprite = UDListSO.uDList[individual.unitId].unitSprite;
        levelText.text = individual.level + "";
        elementCircle.color = ElementColor.GetColor(UDListSO.uDList[individual.unitId].elementEnum);
    }

    public void RecastIcon(int itemId, int itemCount, Sprite itemIcon) //also need reference to image files
    {
        this.itemId = itemId;
        this.itemCount = itemCount;
        image.sprite = itemIcon;
    }

    public void SettingBasedClickEvent()
    {
        switch (iconSetting)
        {
            case IconSetting.collection:
                homeScreen.DisplayUnit(individualData);
                break;
            case IconSetting.dismissal:
                homeScreen.dismissalManager.ChosenUnit(this);
                break;
            case IconSetting.inventory:
                //OPEN ITEM TOOLTIP
                break;
        }
    }
}

