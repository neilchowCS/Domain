using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not used
/// </summary>
public class Tiler : MonoBehaviour
{
    public GameObject tile;
    // Start is called before the first frame update
    public List<GameObject> CreateTiles()
    {
        List<GameObject> output = new List<GameObject>();
        if (tile != null)
        {
            int column = 8;
            int row = 6;
            Vector3 tilerow0col0 = new Vector3(-5.249f, 0f, -4.763f);
            Vector3 tilerow0col1 = new Vector3(-5.249f, 0f, -3.031f);

            Vector3 tilerow1col0 = new Vector3(-3.749f, 0f, -3.897f);
            Vector3 tilerow1col1 = new Vector3(-3.749f, 0f, -2.165f);

            Vector3 tilerow2col0 = new Vector3(-2.25f, 0f, -4.763f);
            Vector3 tilerow2col1 = new Vector3(-2.25f, 0f, -3.031f);


            float initX = -5.249f;
            float distX = -5.249f - (-3.749f);

            float initZ1 = -4.763f;
            float initZ2 = -3.897f;
            float distZ = -4.763f - (-3.031f);

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    GameObject x = Instantiate(tile);
                    if (i%2 == 0)
                    {
                        x.transform.position = new Vector3(initX - (distX * i), 0f, initZ1 - (distZ * j));
                        
                    }
                    else
                    {
                        x.transform.position = new Vector3(initX - (distX * i), 0f, initZ2 - (distZ * j));
                    }
                    output.Add(x);
                }
            }
            
        }
        return output;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
