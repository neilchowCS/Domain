using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapGraph
{
    public static List<MapVertex> GetMap()
    {
        List<MapVertex> map = new();
        for (int i = 0; i < 48; i++)
        {
            map.Add(new());
        }

        map[0].connections = new() { 1, 6 };
        map[1].connections = new() { 0, 2, 6, 7 };
        map[2].connections = new() { 1, 3, 7, 8 };
        map[3].connections = new() { 2, 4, 8, 9 };
        map[4].connections = new() { 3, 5, 9, 10 };
        map[5].connections = new() { 4, 10, 11 };
        map[6].connections = new() { 0, 1, 7, 12, 13 };
        map[7].connections = new() { 1, 2, 6, 8, 13, 14 };
        map[8].connections = new() { 2, 3, 7, 9, 14, 15 };
        map[9].connections = new() { 3, 4, 8, 10, 15, 16 };
        map[10].connections = new() { 4, 5, 9, 11, 16, 17 };
        map[11].connections = new() { 5, 10, 17 };
        map[12].connections = new() { 6, 13, 18 };
        map[13].connections = new() { 6, 7, 12, 14, 18, 19 };
        map[14].connections = new() { 7, 8, 13, 15, 19, 20 };
        map[15].connections = new() { 8, 9, 14, 16, 20, 21 };
        map[16].connections = new() { 9, 10, 15, 17, 21, 22 };
        map[17].connections = new() { 10, 11, 16, 22, 23 };
        map[18].connections = new() { 12, 13, 19, 24, 25 };
        map[19].connections = new() { 13, 14, 18, 20, 25, 26 };
        map[20].connections = new() { 14, 15, 19, 21, 26, 27 };
        map[21].connections = new() { 15, 16, 20, 22, 27, 28 };
        map[22].connections = new() { 16, 17, 21, 23, 28, 29 };
        map[23].connections = new() { 17, 22, 29 };
        map[24].connections = new() { 18, 25, 30 };
        map[25].connections = new() { 18, 19, 24, 26, 30, 31 };
        map[26].connections = new() { 19, 20, 25, 27, 31, 32 };
        map[27].connections = new() { 20, 21, 26, 28, 32, 33 };
        map[28].connections = new() { 21, 22, 27, 29, 33, 34 };
        map[29].connections = new() { 22, 23, 28, 34, 35 };
        map[30].connections = new() { 24, 25, 31, 36, 37 };
        map[31].connections = new() { 25, 26, 30, 32, 37, 38 };
        map[32].connections = new() { 26, 27, 31, 33, 38, 39 };
        map[33].connections = new() { 27, 28, 32, 34, 39, 40 };
        map[34].connections = new() { 28, 29, 33, 35, 40, 41 };
        map[35].connections = new() { 29, 34, 41 };
        map[36].connections = new() { 30, 37, 42 };
        map[37].connections = new() { 30, 31, 36, 38, 42, 43 };
        map[38].connections = new() { 31, 32, 37, 39, 43, 44 };
        map[39].connections = new() { 32, 33, 38, 40, 44, 45 };
        map[40].connections = new() { 33, 34, 39, 41, 45, 46 };
        map[41].connections = new() { 34, 35, 40, 46, 47 };
        map[42].connections = new() { 36, 37, 43 };
        map[43].connections = new() { 37, 38, 42, 44 };
        map[44].connections = new() { 38, 39, 43, 45 };
        map[45].connections = new() { 39, 40, 44, 46 };
        map[46].connections = new() { 40, 41, 45, 47 };
        map[47].connections = new() { 41, 46 };

        int column = 8;
        int row = 6;

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
                if (i % 2 == 0)
                {
                    map[(i * row) + j].Position = new Vector3(initX - (distX * i), 0f, initZ1 - (distZ * j));

                }
                else
                {
                    map[(i * row) + j].Position = new Vector3(initX - (distX * i), 0f, initZ2 - (distZ * j));
                }
            }
        }

        return map;
    }
}
