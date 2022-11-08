using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not used
/// </summary>
public class Tiler : MonoBehaviour
{
    public MapTile tile;
    // Start is called before the first frame update
    public List<MapTile> CreateTiles()
    {
        List<MapTile> output = new();
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
            //1.5

            float initZ1 = -4.763f;
            float initZ2 = -3.897f;
            float distZ = -4.763f - (-3.031f);
            //1.732

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    MapTile x = Instantiate(tile);
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

        string s = "";
        for (int i = 0; i < output.Count; i++)
        {
            s += $"map[{i}].connections = new() {{ ";
            List<int> list = new();
            for (int j = 0; j < output.Count; j++)
            {
                if (j != i && Vector3.Distance(output[i].transform.position, output[j].transform.position) < 2)
                {
                    list.Add(j);
                }
            }

            while (list.Count > 1)
            {
                s += $" {list[0]},";
                list.RemoveAt(0);
            }
            s += $" {list[0]} }}; \n";
        }

        Debug.Log(s);



        return output;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
