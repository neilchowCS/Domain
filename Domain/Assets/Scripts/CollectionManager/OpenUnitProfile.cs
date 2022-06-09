using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenUnitProfile : MonoBehaviour
{
    public HomeScreen homeScreen;
    public Image image;
    public Image elementCircle;
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitButton(Sprite sprite, int level, Color color)
    {
        image.sprite = sprite;
        levelText.text = level + "";
        elementCircle.color = color;
    }

    public void OpenProfile()
    {
        homeScreen.DisplayUnit(homeScreen.collectionManager.
            collection.individualDataList[transform.GetSiblingIndex()]);
    }
}
