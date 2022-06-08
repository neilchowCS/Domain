using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public CollectionManager collectionManager;
    public SummonManager summonManager;
    public UnitProfileDisplay unitDisplay;
    public GameObject currentActive;

    // Start is called before the first frame update
    void Start()
    {
        //QUESTION why does initializing player collection here not work?
        currentActive = this.gameObject;
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
        Debug.Log(individualData.unitId);
        unitDisplay.gameObject.SetActive(true);
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
}
