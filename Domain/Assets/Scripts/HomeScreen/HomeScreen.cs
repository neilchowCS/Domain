using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public GameObject collectionManager;
    public GameObject unitDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCollection()
    {
        collectionManager.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void HideCollection()
    {
        this.gameObject.SetActive(true);
        collectionManager.SetActive(false);
    }

    public void DisplayUnit()
    {
        unitDisplay.SetActive(true);
        collectionManager.SetActive(false);
    }

    public void HideUnit()
    {
        collectionManager.SetActive(true);
        unitDisplay.SetActive(false);
    }
}
