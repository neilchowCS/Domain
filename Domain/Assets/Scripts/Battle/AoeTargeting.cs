using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AoeTargeting
{
    static public Vector3 GetAoeLocation(this BattleUnit unit, float radius, float range)
    {
        List<Circle> circleList = GetAllCircles(unit, radius);
        int index = 0;
        int[] counts = new int[circleList.Count];
        //points in each circle
        foreach (Circle circle in circleList)
        {
            foreach (Vector3 point in BUnitHelperFunc.GetEnemyPositionList(unit))
            {
                if (Vector3.Distance(point, circle.center) <= radius)
                {
                    counts[index] += 1;
                }
            }
            index++;
        }

        int max = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            if (counts[i] > max)
            {
                max = counts[i];
            }
        }

        List<int> indices = new List<int>();
        for (int i = 0; i < counts.Length; i++)
        {
            if (counts[i] == max)
            {
                indices.Add(i);
            }
        }

        indices = indices.OrderByDescending(o => Vector3.Distance(circleList[o].chord1, circleList[o].chord2)).ToList();

        return circleList[indices[0]].center;
    }

    static private List<Circle> GetAllCircles(BattleUnit battleUnit, float radius)
    {
        float diameter = radius * 2;
        List<Circle> output = new List<Circle>();
        List<Vector3> pointList = BUnitHelperFunc.GetEnemyPositionList(battleUnit);

        for (int i = 0; i < pointList.Count; i++)
        {
            for (int j = 0; j < pointList.Count; j++)
            {
                if (i != j && Vector3.Distance(pointList[i], pointList[j]) <= diameter)
                {
                    (Vector3, Vector3) calcCenter = GetCentersFromChord(pointList[i], pointList[j], radius);
                    output.Add(new Circle(calcCenter.Item1, radius, pointList[i], pointList[j]));
                    output.Add(new Circle(calcCenter.Item2, radius, pointList[i], pointList[j]));
                }
            }
        }
        return output;
    }

    static private(Vector3, Vector3) GetCentersFromChord(Vector3 point1, Vector3 point2, float radius)
    {
        float diameter = 2 * radius;
        Vector3 midPoint = (point1 + point2) / 2;

        float temp = Vector3.Distance(point1, point2) / 2;
        float chordMultiple = temp * temp;

        //Debug.Log("intersect: " + temp);
        //Debug.Log("chordMultiple: " + chordMultiple);
        //chord = (diameter - x)x
        float positiveSolution = 0;

        /*
        float a = -1;
        float b = diameter;
        float c = -chordMultiple;

        float determinant = (b * b) - (4 * a * c);
        */

        float determinant = (diameter * diameter) - (4 * -1 * -chordMultiple);

        if (determinant >= 0)
        {
            float solution1 = (-diameter + Mathf.Sqrt(determinant)) / (-2);
            float solution2 = (-diameter - Mathf.Sqrt(determinant)) / (-2);

            //Debug.Log(solution1);
            //Debug.Log(solution2);

            if (solution1 < diameter && solution1 > 0)
            {
                positiveSolution = solution1;
            }
            else if (solution2 < diameter && solution2 > 0)
            {
                positiveSolution = solution2;
            }
        }

        //Vector3 direction = point1 - point2;
        Vector3 perp = Vector3.Cross(point1 - point2, new Vector3(0, 0, 1));

        /*
        Vector3 border = midPoint + (perp.normalized * positiveSolution);
        tempPoint2.transform.localPosition = border;
        */

        return (midPoint + (perp.normalized * (radius - positiveSolution)),
            midPoint - (perp.normalized * (radius - positiveSolution)));
    }
}
