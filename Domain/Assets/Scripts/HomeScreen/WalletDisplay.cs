using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    public ResourceHandler resourceHandler;
    public TextMeshProUGUI text;
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
        PlayerData data = resourceHandler.playerData;
        text.text = "Gold: " + data.gold + " Essence: " + data.essence;
    }
}
