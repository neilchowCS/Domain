using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerMain : MonoBehaviour
{
    public List<PlayerData> database;
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.singleton.StartServer();
        PlayerDatabase pData = new();
        string json = JsonUtility.ToJson(database, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Server/Database.json", json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
