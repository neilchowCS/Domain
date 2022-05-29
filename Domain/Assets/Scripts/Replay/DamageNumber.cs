using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageNumber : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Color color;
    private float alphaSpeed = 3;
    private float moveSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        color = textMesh.color;
        color.a -= alphaSpeed * Time.deltaTime;
        if (color.a < 0)
        {
            Destroy(gameObject);
            return;
        }
        textMesh.color = color;
        this.transform.localPosition = this.transform.localPosition
            + new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
