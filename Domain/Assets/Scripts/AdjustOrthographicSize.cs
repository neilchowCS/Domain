using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustOrthographicSize : MonoBehaviour
{
    public Camera orthoCamera;
    // Start is called before the first frame update
    void Start()
    {
        orthoCamera.orthographicSize = (orthoCamera.orthographicSize * (9f / 16f))
            / ((float)Screen.width/(float)Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
