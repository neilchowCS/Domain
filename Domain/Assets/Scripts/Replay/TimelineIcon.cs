using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineIcon : MonoBehaviour
{
    //IDENTIFICATION
    public int id;
    public Image image;

    public float speed;
    public Vector3 destination;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (this.transform.localPosition.x < destination.x)
            {
                this.transform.localPosition = new Vector3((100 - (100 / 2f)) * 12.6f, 0, 0); ;
                isMoving = false;
                this.enabled = false;
            }
                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, destination, speed * Time.deltaTime);
                if (Mathf.Abs(this.transform.position.x - destination.x) < 0.001f)
                {
                    this.transform.localPosition = destination;
                    isMoving = false;
                    this.enabled = false;
                }
        }
    }

    public void StartMovement(Vector3 destination, float speed)
    {
        this.destination = destination;
        this.speed = speed;
        isMoving = true;
        this.enabled = true;
    }
}
