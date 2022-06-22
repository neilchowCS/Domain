using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public CollectionManager collectionManager;
    public SummonManager summonManager;
    public UnitProfileDisplay unitDisplay;
    public DismissalManager dismissalManager;
    public ResourceManager resourceManager;

    public UDListScriptableObject uDListSO;

    // Start is called before the first frame update
    void Start()
    {
        //QUESTION why does initializing player collection here not work?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCollection()
    {
        collectionManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideCollection()
    {
        this.gameObject.SetActive(true);
        collectionManager.gameObject.SetActive(false);
    }

    public void DisplayUnit(UnitIndividualData individualData)
    {
        //Debug.Log(individualData.unitId);
        unitDisplay.gameObject.SetActive(true);
        unitDisplay.SetUnit(individualData);
        collectionManager.gameObject.SetActive(false);
    }

    public void HideUnit()
    {
        collectionManager.gameObject.SetActive(true);
        unitDisplay.gameObject.SetActive(false);
    }

    public void ShowSummon()
    {
        summonManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideSummon()
    {
        this.gameObject.SetActive(true);
        summonManager.gameObject.SetActive(false);
    }

    public void ShowAltar()
    {
        dismissalManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideAltar()
    {
        this.gameObject.SetActive(true);
        dismissalManager.gameObject.SetActive(false);
    }

    public void ShowResources()
    {
        resourceManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideResources()
    {
        this.gameObject.SetActive(true);
        resourceManager.gameObject.SetActive(false);
    }
}
