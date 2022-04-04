using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRays : MonoBehaviour
{
    public Ray ray;
    public Vector3 screenPos;
    public Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        screenPos = main.WorldToScreenPoint(this.gameObject.transform.position);
        Debug.Log(screenPos.x + ", " + screenPos.y + ", " + screenPos.z);
        ray = new Ray(main.transform.position,
            (this.gameObject.transform.position - main.transform.position)/Vector3.Distance(this.gameObject.transform.position,main.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.white);
    }
}
