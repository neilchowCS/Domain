using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    //Pages
    public CampaignManager campaignHandler;
    public CollectionManager collectionManager;
    public SummonManager summonManager;
    public UnitProfileDisplay unitDisplay;
    public DismissalManager dismissalManager;
    public TowerManager towerManager;
    //Overlays
    public ReplayHandler replayUI;
    public TeamBuildManager teamBuildManager;

    public UDListScriptableObject uDListSO;

    //IF STACK IS EMPTY, ASSUME CURRENT PAGE IS HOME
    public Stack<GameObject> UIStack;


    // Start is called before the first frame update
    
    void Awake()
    {
        //QUESTION why does initializing player collection here (start not awake) not work?
        UIStack = new();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetUI(string page)
    {
        switch (page)
        {
            case "Home":
                return this.gameObject;
            case "Campaign":
                return campaignHandler.gameObject;
            case "Altar":
                return dismissalManager.gameObject;
            case "Summon":
                return summonManager.gameObject;
            case "Collection":
                return collectionManager.gameObject;
            case "Unit":
                return unitDisplay.gameObject;
            case "Tower":
                return towerManager.gameObject;
            case "Replay":
                return replayUI.gameObject;
            case "Teambuilder":
                return teamBuildManager.gameObject;
            default:
                return null;
        }
    }

    public void OpenPage(string toOpen)
    {
        if (GetUI(toOpen) != null)
        {
            if (UIStack.Count > 0)
            {
                UIStack.Peek().SetActive(false);
            }
            UIStack.Push(GetUI(toOpen));
            UIStack.Peek().SetActive(true);
        }
    }

    public void ClosePage()
    {
        if (UIStack.Count > 1)
        {
            UIStack.Pop().SetActive(false);
            UIStack.Peek().SetActive(true);
        }
        else
        {
            UIStack.Pop().SetActive(false);
            this.gameObject.SetActive(true);
        }
    }

    public void OpenOverlay(string toOpen)
    {
        GetUI(toOpen)?.SetActive(true);
    }

    public void CloseOverlay(string toClose)
    {
        GetUI(toClose)?.SetActive(false);
    }

    public void DisplayUnit(UnitIndividualData individualData)
    {
        //Debug.Log(individualData.unitId);
        
        unitDisplay.SetUnit(individualData);

        OpenPage("Unit");
    }
}
