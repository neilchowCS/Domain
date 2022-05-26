using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharGridMarker : MonoBehaviour
{
    public Image image;
    public (UnitDataScriptableObject, UnitIndividualData) compositeData;
    public int positionId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitial(Vector3 worldPosition,
        (UnitDataScriptableObject, UnitIndividualData) compositeData, int id)
    {
        this.transform.position = worldPosition;
        this.compositeData = compositeData;
        image.sprite = compositeData.Item1.unitSprite;
        positionId = id;
    }

    public void SetEnemyInitial(Vector3 localPosition, Sprite sprite)
    {
        this.transform.localPosition = localPosition;
        image.sprite = sprite;
    }
}
