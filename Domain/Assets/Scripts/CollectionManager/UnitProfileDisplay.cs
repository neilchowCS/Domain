using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitProfileDisplay : MonoBehaviour
{
    public TextMeshProUGUI minimizeButtonText;
    public List<GameObject> minimizableObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
