using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update

    private void Awake()
    {
        ReadFromJSON();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadFromJSON()
    {
        playerData = DataSerialization.DeserializeStaticPlayerData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json"));
    }

    public void WritePlayerData()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json",
                DataSerialization.SerializeStaticPlayerData(playerData));
    }
}
