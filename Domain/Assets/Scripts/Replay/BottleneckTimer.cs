using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleneckTimer : MonoBehaviour
{
    public float timer = 0;
    public float bottleneckTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bottleneckTime)
        {
            timer = 0;
            this.enabled = false;
            BottleneckDone();
        }
    }

    public void BottleneckDone()
    {

    }
}
