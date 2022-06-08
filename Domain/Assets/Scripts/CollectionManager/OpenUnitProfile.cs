using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUnitProfile : MonoBehaviour
{
    public HomeScreen homeScreen;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitButton(HomeScreen homeScreen, Sprite sprite)
    {
        image.sprite = sprite;
        this.homeScreen = homeScreen;
    }

    public void OpenProfile()
    {
        homeScreen.DisplayUnit();
    }
}
