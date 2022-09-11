using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public ResourceHandler resourceHandler;
    public TeamBuildManager teamBuildManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStage()
    {
        teamBuildManager.stageId = resourceHandler.playerData.currentStage + 1;
    }
}
