using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class ElementColor
{
    public static Color GetColor(ElementEnum elementEnum)
    {
        switch (elementEnum)
        {
            case ElementEnum.fire:
                return Color.red;
            case ElementEnum.water:
                return Color.blue;
            case ElementEnum.nature:
                return Color.green;
            case ElementEnum.light:
                return Color.yellow;
            case ElementEnum.dark:
                return Color.black;
        }
        return Color.white;
    }
}