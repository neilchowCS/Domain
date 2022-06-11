using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseUnitIcon : MonoBehaviour
{
    public UnitIndividualData individualData;
    public Image image;
    public Image elementCircle;
    public TextMeshProUGUI levelText;

    public enum IconSetting { nil, collection, dismissal }
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

    public void InitButton(HomeScreen homeScreen, IconSetting setting,
        UDListScriptableObject UDListSO, UnitIndividualData individual)
    {
        this.homeScreen = homeScreen;
        iconSetting = setting;
        InitButton(UDListSO, individual);
    }

    public void InitButton(HomeScreen homeScreen, IconSetting setting,
        Sprite sprite, int level, Color color)
    {
        this.homeScreen = homeScreen;
        iconSetting = setting;
        InitButton(sprite, level, color);
    }

    public void InitButton(UDListScriptableObject UDListSO, UnitIndividualData individual)
    {
        individualData = individual;
        image.sprite = UDListSO.uDList[individual.unitId].unitSprite;
        levelText.text = individual.level + "";
        elementCircle.color = ElementColor.GetColor(UDListSO.uDList[individual.unitId].elementEnum);
    }

    public void InitButton(Sprite sprite, int level, Color color)
    {
        image.sprite = sprite;
        levelText.text = level + "";
        elementCircle.color = color;
    }

    public void SettingBasedClickEvent()
    {
        switch (iconSetting)
        {
            case IconSetting.collection:
                homeScreen.DisplayUnit(homeScreen.collectionManager.
                    collectionHandler.collection.individualDataList[transform.GetSiblingIndex()]);
                break;
            case IconSetting.dismissal:
                homeScreen.dismissalManager.ChosenUnit(this);
                break;
        }
    }
}

