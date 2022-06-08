using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public CollectionManager collectionManager;
    public GameObject unitDisplay;

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

    public void DisplayUnit()
    {
        unitDisplay.SetActive(true);
        collectionManager.gameObject.SetActive(false);
    }

    public void HideUnit()
    {
        collectionManager.gameObject.SetActive(true);
        unitDisplay.SetActive(false);
    }
}
