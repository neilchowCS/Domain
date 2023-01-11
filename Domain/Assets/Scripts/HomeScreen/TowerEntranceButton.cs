using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntranceButton : MonoBehaviour
{

    public TowerEntrance parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        parent.parent.home.teamBuildManager.stageId = parent.stageId; //teamBuildManager.stageId = resourceHandler.playerData.currentStage + 1;
        parent.parent.home.teamBuildManager.Init();
        parent.parent.home.OpenOverlay("Teambuilder");
    }
}
