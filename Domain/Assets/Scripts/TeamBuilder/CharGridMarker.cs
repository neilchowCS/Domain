using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharGridMarker : MonoBehaviour
{
    public Image image;
    public UnitDataScriptableObject unitData;
    public int positionId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitial(Vector3 worldPosition, UnitDataScriptableObject data, int id)
    {
        this.transform.position = worldPosition;
        unitData = data;
        image.sprite = data.unitSprite;
        positionId = id;
    }
}
