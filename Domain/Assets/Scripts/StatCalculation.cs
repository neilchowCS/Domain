using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatCalculation
{
    public static int CalcStat(int level, int baseStat)
    {
        float levelMultiplier = 1 + ((level - 1) * 0.085f);
        return (int)(baseStat * levelMultiplier);
    }

    public static float CalcFloat()
    {
        return 0;
    }
}