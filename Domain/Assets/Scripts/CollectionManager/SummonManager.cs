using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummonManager : MonoBehaviour
{
    public int numberOfUnits = 4;
    public TextMeshProUGUI summonDisplay;

    public CollectionHandler collectionHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformSummon()
    {
        int output = Random.Range(0, numberOfUnits);
        DisplaySummon(output);
        UnitIndividualData individualData = new UnitIndividualData(output, 1);
        collectionHandler.collection.individualDataList.Add(individualData);
        collectionHandler.WriteCollection();
    }

    public void DisplaySummon(int i)
    {
        summonDisplay.text = i + "";
    }
}
