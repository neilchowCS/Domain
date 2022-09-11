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

    public UDListScriptableObject uDListSO;

    public string currentPage;

    // Start is called before the first frame update
    void Awake()
    {
        //QUESTION why does initializing player collection here (start not awake) not work?
        currentPage = "Home";
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
            default:
                return null;
        }
    }

    public void ChangePage(string toOpen)
    {
        if (GetUI(currentPage) != null && GetUI(toOpen) != null)
        {
            GetUI(currentPage).SetActive(false);
            currentPage = toOpen;
            GetUI(toOpen).SetActive(true);
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

        ChangePage("Unit");
    }
}
