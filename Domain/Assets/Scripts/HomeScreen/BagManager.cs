using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    /*
    TABS:
    Miscellaneous items
    Unit shards
    Equipment
    Chests
    */
    public HomeScreen homeScreen;
    public ResourceHandler resourceHandler;
    public TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerData data = resourceHandler.playerData;
        displayText.text = "Item0: " + data.playerInventory[0] + "\n"
            + "Item1: " + data.playerInventory[1];
    }
}
