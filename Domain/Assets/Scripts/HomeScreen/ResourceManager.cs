using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public ResourceHandler resourceHandler;
    public System.DateTime dateTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClaimReward()
    {
        resourceHandler.playerData.lastClaim = System.DateTime.UtcNow;
        resourceHandler.playerData.gold += 1000;
        resourceHandler.playerData.essence += 1000;
        resourceHandler.WritePlayerData();
    }
}
