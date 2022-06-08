using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public HomeScreen homeScreen;
    public GameObject gridParent;
    public OpenUnitProfile unitProfileButtonPrefab;
    public UDListScriptableObject UDListScriptableObject;

    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCollection()
    {
        DataSerialization serializer = new DataSerialization();
        PlayerCollectionData collection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));
        foreach (UnitIndividualData data in collection.individualDataList)
        {
            OpenUnitProfile x = Instantiate(unitProfileButtonPrefab, gridParent.transform);
            //FIXME should be manager not homescreen
            x.InitButton(this.homeScreen, UDListScriptableObject.uDList[data.unitId].unitSprite);
        }
    }
}
