using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReplayProfile : MonoBehaviour
{
    public int globalId;
    public TextMeshProUGUI profileName;
    public TextMeshProUGUI damageNumber;
    public GameObject bar;
    public int damageInt;

    // Start is called before the first frame update
    void Start()
    {
        damageInt = 0;
        damageNumber.text = "" + damageInt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        profileName.text = name;
    }

    public void IncreaseDamage(int amount)
    {
        damageInt += amount;
        damageNumber.text = "" + damageInt;
    }
}
