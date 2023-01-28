using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour
{
    public int numberOfUnits = 4;
    private int tickets = 0;
    public TextMeshProUGUI summonDisplay;
    public TextMeshProUGUI ticketDisplay;
    public Image summonImage;

    public CollectionHandler collectionHandler;
    public ResourceHandler resourceHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        summonImage.gameObject.SetActive(false);
        summonDisplay.text = "";

        tickets = resourceHandler.playerData.playerInventory[0];
        ticketDisplay.text = $"Tickets Owned: {tickets}";
    }

    public void PerformSummon()
    {
        if (tickets > 0)
        {
            tickets--;
            resourceHandler.playerData.playerInventory[0] = tickets;
            resourceHandler.WritePlayerData();

            int output = Random.Range(0, numberOfUnits);
            DisplaySummon(output);
            UnitIndividualData individualData = new UnitIndividualData(output, 1);
            collectionHandler.collection.individualDataList.Add(individualData);
            collectionHandler.WriteCollection();
        }
    }

    public void DisplaySummon(int i)
    {
        summonImage.gameObject.SetActive(true);
        summonDisplay.text = i + "";
        summonImage.sprite = collectionHandler.uDListSO.uDList[i].unitSprite;

        tickets = resourceHandler.playerData.playerInventory[0];
        ticketDisplay.text = $"Tickets Owned: {tickets}";
    }
}
