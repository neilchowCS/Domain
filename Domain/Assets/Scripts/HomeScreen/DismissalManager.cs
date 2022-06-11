using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Banish unit?
public class DismissalManager : MonoBehaviour
{
    public UDListScriptableObject UDListScriptableObject;

    public PlayerData playerData;
    public CollectionHandler collectionHandler;

    public GameObject gridParent;
    public GameObject selectedParent;

    public List<BaseUnitIcon> unitButtonList;
    public BaseUnitIcon dismissButtonPrefab;

    public List<BaseUnitIcon> selectedButtonList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public void ChosenUnit(BaseUnitIcon dismissalIcon)
    {
        dismissalIcon.transform.SetParent(selectedParent.transform);
        selectedButtonList.Add(dismissalIcon);
    }
}
