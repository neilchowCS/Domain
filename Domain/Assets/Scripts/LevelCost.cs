using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelCost
{
    static int[] costAtEachLevel = new int[50] { 27, 51, 104, 181, 287, 417, 574, 758, 966, 1200, 1407, 1541, 1696, 1926, 2154, 2407, 2514, 2792, 2991, 3118, 3237, 3448, 3682, 3873, 4204, 4517, 4837, 4999, 5319, 5625, 5766, 6105, 6383, 6609, 6865, 7006, 7219, 7332, 7537, 7837, 8104, 8476, 8757, 9010, 9288, 9454, 9656, 9811, 10150, 10440 };

    public static int GetCost(int unitLevel)
    {
        /*
        if (costAtEachLevel.Count == 0)
        {
            int lastVal = 0;
            costAtEachLevel.Add(25);
            for (int j = 0; j < 9; j++)
            {
                float mod = (235.78f / 9) * (j + 1);
                costAtEachLevel.Add(costAtEachLevel[^1] + (int)mod);
            }
            lastVal = costAtEachLevel[^1];
            float modifier = 235.78f;
            for (int j = 0; j < 40; j++)
            {
                costAtEachLevel.Add((int) (costAtEachLevel[^1] + (modifier) + Random.Range(-(modifier/1.7f), (modifier / 1.7f))));
            }
        }
        string output = "";
        for (int j = 0; j < costAtEachLevel.Count; j++ )
        {
            costAtEachLevel[j] = costAtEachLevel[j] + Random.Range(0, 4);
            //Debug.Log(costAtEachLevel[j]);
            output = output + costAtEachLevel[j] + ", ";
        }
        Debug.Log(output);
        */
        if (unitLevel > costAtEachLevel.Length)
        {
            return costAtEachLevel[^1];
        }
        return costAtEachLevel[unitLevel - 1];
    }

    public static int GetSalePrice(int i)
    {
        int baseOutput = 10;
        for (int j = 1; j < i; j++)
        {
            baseOutput += GetCost(i);
        }
        return (int)(baseOutput * 0.66f);
    }
}
