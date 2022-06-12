using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitProfileDisplay : MonoBehaviour
{
    public TextMeshProUGUI minimizeButtonText;
    public List<GameObject> minimizableObjects;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelDisplayText;
    public TextMeshProUGUI goldCostText;
    public TextMeshProUGUI essenceCostText;

    //public UDListScriptableObject UDListSO;
    public UnitIndividualData individualData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUnit(UDListScriptableObject uDListSO, UnitIndividualData data)
    {
        individualData = data;
        nameText.text = uDListSO.uDList[data.unitId].unitName;
        levelDisplayText.text = $"{data.level}/100";
        goldCostText.text = $"{NumberFormatter.Format(LevelCost.GetCost(data.level))} <b>G";
        essenceCostText.text = $"{NumberFormatter.Format((int)(LevelCost.GetCost(data.level) * 1.5f))} <b>E";

    }

    public void ToggleFullShowImage()
    {
        if (minimizeButtonText.text == "+")
        {
            minimizeButtonText.text = "-";
        }
        else
        {
            minimizeButtonText.text = "+";
        }
        foreach (GameObject x in minimizableObjects)
        {
            x.SetActive(!x.activeInHierarchy);
        }
    }
}
