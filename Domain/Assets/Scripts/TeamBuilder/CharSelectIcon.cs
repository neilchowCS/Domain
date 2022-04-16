using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectIcon : MonoBehaviour
{
    public Vector3 initialPos;
    private bool drag = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
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
            this.transform.position = initialPos;
            drag = false;
        }
    }
}
