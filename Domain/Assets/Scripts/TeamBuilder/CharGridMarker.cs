using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharGridMarker : MonoBehaviour
{
    public Image image;
    public UnitIndependentData indData;
    public int positionId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitial(Vector3 worldPosition, UnitIndependentData indData, int id)
    {
        this.transform.position = worldPosition;
        this.indData = indData;
        image.sprite = indData.baseData.unitSprite;
        positionId = id;
    }
}
