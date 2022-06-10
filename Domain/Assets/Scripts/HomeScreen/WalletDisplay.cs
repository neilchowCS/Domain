using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
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
        DataSerialization serializer = new DataSerialization();
        PlayerData data = serializer.DeserializePlayerData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));
        text.text = "Gold: " + data.gold + " Essence: " + data.essence;
    }
}
