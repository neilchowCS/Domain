using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandFieldShowEnemy : MonoBehaviour
{
    public float speed;
    public Vector3 minimizedPosition;
    public Vector3 expandedPosition;
    public bool isMinimized;

    // Start is called before the first frame update
    void Start()
    {
        minimizedPosition = new Vector3(0, 0, 0);
        expandedPosition = new Vector3(-1600, 0, 0);
        isMinimized = true;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMinimized)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, expandedPosition, speed * Time.deltaTime);
            if (this.transform.localPosition.x - expandedPosition.x < 0.01)
            {
                this.enabled = false;
            }
        }
        else
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, minimizedPosition, speed * Time.deltaTime);
            if (minimizedPosition.x - this.transform.localPosition.x < 0.01)
            {
                this.enabled = false;
            }
        }
    }

    public void MinimizeField()
    {
        this.enabled = true;
        isMinimized = false;
    }

    public void MaximizeField()
    {
        this.enabled = true;
        isMinimized = true;
    }
}
