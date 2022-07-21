using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AoeTargetingExtension
{
    public static class AoeTargeting
    {
        static private float margin = 0.002f;
        static private float minSubdivisions = 0.01f;

        static private List<Vector3> GetEnemyPositionList(IBattleUnit unit)
            => unit.Executor.GetEnemyUnits(unit).Select(o => o.Position).ToList();

        static public Vector3 GetAoeLocation(this IBattleUnit unit, float radius, float range)
        {
            List<Vector3> enemyPositions = GetEnemyPositionList(unit);

            if (enemyPositions.Count == 1)
            {
                return enemyPositions[0];
            }

            if (enemyPositions.Count == 2 && Vector3.Distance(enemyPositions[0], enemyPositions[1]) <= radius)
            {
                return (enemyPositions[0] + enemyPositions[1]) / 2;
            }

            List<Circle> circleList = GetAllCircles(unit, radius);

            if (circleList.Count > 0)
            {

                int index = 0;
                int[] counts = new int[circleList.Count];
                //points in each circle
                foreach (Circle circle in circleList)
                {
                    foreach (Vector3 point in GetEnemyPositionList(unit))
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

                Circle finalCircle = RecenterCircle(circleList[indices[0]], unit);

                //Debug.Log(finalCircle.center.x + "x, " + finalCircle.center.z + "z");

                return finalCircle.center;
            }
            return enemyPositions[0];
        }

        static private List<Circle> GetAllCircles(IBattleUnit battleUnit, float radius)
        {
            float diameter = radius * 2;
            List<Circle> output = new List<Circle>();
            List<Vector3> pointList = GetEnemyPositionList(battleUnit);

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

        static private (Vector3, Vector3) GetCentersFromChord(Vector3 point1, Vector3 point2, float radius)
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
            Vector3 perp = Vector3.Cross(point1 - point2, new Vector3(0, 1, 0));

            /*
            Vector3 border = midPoint + (perp.normalized * positiveSolution);
            tempPoint2.transform.localPosition = border;
            */

            return (midPoint + (perp.normalized * (radius - positiveSolution)),
                midPoint - (perp.normalized * (radius - positiveSolution)));
        }

        static private List<Vector3> GetPointsInCircle(Circle circle, List<Vector3> points)
        {
            List<Vector3> output = new List<Vector3>();
            for (int i = 0; i < points.Count; i++)
            {
                if (Vector3.Distance(points[i], circle.center) <= circle.radius + margin)
                {
                    output.Add(points[i]);
                }
            }
            return output;
        }

        static private Circle RecenterCircle(Circle circle, IBattleUnit battleUnit)
        {
            List<Vector3> points = GetPointsInCircle(circle, GetEnemyPositionList(battleUnit));
            if (points.Count <= 2)
            {
                //Debug.Log("insufficient");
                if (points.Count == 1) return circle;
                return new Circle((points[0] + points[1]) / 2, circle.radius, circle.chord1, circle.chord2);
            }

            //float span = Vector3.Distance(circle.chord1, circle.chord2)/4;
            int count = 1;
            return Subdivide(circle, Vector3.Distance(circle.chord1, circle.chord2) / 4, points, false, count);
        }

        //subdivision = 0
        static private Circle Subdivide(Circle circle, float span, List<Vector3> points, bool exceeded, int count)
        {
            if (span < minSubdivisions / 2)
            {
                //Debug.Log(count);
                if (!exceeded)
                {
                    return circle;
                }
                else
                {
                    return AdjustCenter(circle, circle.radius + (span * 2));
                }
            }

            if (!exceeded)
            {
                Circle newCircle = AdjustCenter(circle, circle.radius - span);
                if (GetPointsInCircle(newCircle, points).Count == points.Count)
                {
                    return Subdivide(newCircle, span / 2, points, false, count + 1);
                }
                else
                {
                    return Subdivide(newCircle, span / 2, points, true, count + 1);
                }
            }
            else
            {
                Circle newCircle = AdjustCenter(circle, circle.radius + span);
                if (GetPointsInCircle(newCircle, points).Count == points.Count)
                {
                    return Subdivide(newCircle, span / 2, points, false, count + 1);
                }
                else
                {
                    return Subdivide(newCircle, span / 2, points, true, count + 1);
                }
            }
        }

        static private Circle AdjustCenter(Circle oldCircle, float newRadius)
        {
            (Vector3, Vector3) newCenters = GetCentersFromChord(oldCircle.chord1, oldCircle.chord2, newRadius);
            if (Vector3.Distance(newCenters.Item1, oldCircle.center) < Vector3.Distance(newCenters.Item2, oldCircle.center))
            {
                return new Circle(newCenters.Item1, newRadius, oldCircle.chord1, oldCircle.chord2);
            }
            return new Circle(newCenters.Item2, newRadius, oldCircle.chord1, oldCircle.chord2);
        }

    }
}