using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMapGenerator
{

    public static Hexagon[,] GenerateHexMap()
    {
        int xMax = 8;
        int yMax = 6;

        Hexagon[,] map = new Hexagon[xMax,yMax];

        float initX = -5.249f;
        float distX = -5.249f - (-3.749f);
        //1.5

        float initZ1 = -4.763f;
        float initZ2 = -3.897f;
        float distZ = -4.763f - (-3.031f);
        //1.732

        for (int i = 0; i < xMax; i++)
        {
            for (int j = 0; j < yMax; j++)
            {
                if (i % 2 == 0)
                {
                    map[i, j] = new Hexagon(new Vector3(initX - (distX * i), 0f, initZ1 - (distZ * j)));

                }
                else
                {
                    map[i, j] = new Hexagon(new Vector3(initX - (distX * i), 0f, initZ2 - (distZ * j)));
                }
            }
        }

        return map;
    }
}
