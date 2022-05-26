using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharSelectIcon : MonoBehaviour
{
    public TeamBuildManager manager;
    public (UnitDataScriptableObject, UnitIndividualData) compositeData;
    public Vector3 initialPos;
    public Image image;
    public TextMeshProUGUI levelText;

    public bool used = false;

    private bool drag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDrag()
    {
        if (!used)
        {
            Vector3 newPos = Input.mousePosition;
            newPos.z = 10;
            this.transform.position = Camera.main.ScreenToWorldPoint(newPos);
            drag = true;
        }
    }

    public void OnMouseUp()
    {
        if (drag && !used)
        {
            manager.IconReleased(this);
            this.transform.position = initialPos;
            drag = false;
        }
    }

    public void SetInitial(Vector3 pos, (UnitDataScriptableObject, UnitIndividualData) compositeData)
    {
        this.transform.localPosition = pos;
        initialPos = this.transform.position;
        this.compositeData = compositeData;
        image.sprite = compositeData.Item1.unitSprite;
        levelText.text = compositeData.Item2.level + "";
    }

    public void CharUsed()
    {
        used = true;
        image.color = Color.gray;
    }
}
