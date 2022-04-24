using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectIcon : MonoBehaviour
{
    public TeamBuildManager manager;
    public UnitIndependentData indData;
    public Vector3 initialPos;
    public Image image;

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
        Vector3 newPos = Input.mousePosition;
        newPos.z = 10;
        this.transform.position = Camera.main.ScreenToWorldPoint(newPos);
        drag = true;
    }

    public void OnMouseUp()
    {
        if (drag)
        {
            manager.IconReleased(this);
            this.transform.position = initialPos;
            drag = false;
        }
    }

    public void SetInitial(Vector3 pos, UnitIndependentData indData)
    {
        this.transform.localPosition = pos;
        initialPos = this.transform.position;
        this.indData = indData;
        image.sprite = indData.baseData.unitSprite;
    }
}
