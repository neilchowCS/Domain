using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberFormatter
{
    public static string Format(int i)
    {
        if (i > 10000)
        {
            return i / 1000 + "k";
        }
        return i + "";
    }
}
