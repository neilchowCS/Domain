using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public int state = 0;
    public GameObject blue;
    public GameObject red;

    public void Awake()
    {
        this.enabled = false;
    }

    public void Update()
    {
        switch (state)
        {
            case 1:
                MeshRenderer renderer = blue.GetComponent<MeshRenderer>();
                Color c = renderer.material.color;
                c.a -= 2f * Time.deltaTime;
                renderer.material.color = c;
                if (c.a <= .2f)
                {
                    this.enabled = false;
                    Reset();
                }
                break;
            case 2:
                MeshRenderer renderer2 = red.GetComponent<MeshRenderer>();
                Color c2 = renderer2.material.color;
                c2.a -= 2f * Time.deltaTime;
                renderer2.material.color = c2;
                if (c2.a <= .2f)
                {
                    this.enabled = false;
                    Reset();
                }
                break;
            default:
                this.enabled = false;
                Reset();
                return;
        }
    }

    public void Reset()
    {
        blue.SetActive(false);
        red.SetActive(false);
    }

    public void SetBlue()
    {
        MeshRenderer renderer = blue.GetComponent<MeshRenderer>();
        Color c = renderer.material.color;
        c.a = 1f;
        renderer.material.color = c;
        this.enabled = true;
        state = 1;
        blue.SetActive(true);
    }

    public void SetRed()
    {
        MeshRenderer renderer = red.GetComponent<MeshRenderer>();
        Color c = renderer.material.color;
        c.a = 1f;
        renderer.material.color = c;
        this.enabled = true;
        state = 2;
        red.SetActive(true);
    }
}
