using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTestMain : MonoBehaviour
{
    private List<Vector3> pointList;
    //Inspector
    public GameObject prefabPoint;
    private List<GameObject> pointObjects;
    public GameObject tempPoint;

    public GameObject tempPoint2;
    public GameObject tempPoint3;

    private const int NumPoints = 50;

    public LineRenderer lineRenderer;
    public LineRenderer lineRendererBlue;

    public Vector3 rayDirection;
    public Vector3 rayOrigin;
    public float scaleFactor;



    void Start()
    {
        GameObject temp = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
        temp.transform.localPosition = new Vector3(1, 0, 0);
        scaleFactor = temp.transform.position.x;
        Destroy(temp);

        Execute();
    }

    private void Update()
    {
        Debug.DrawRay(rayOrigin, rayDirection, Color.black);
    }

    public void Execute()
    {
        pointList = new List<Vector3>();
        for (int i = 0; i < NumPoints; i++)
        {
            pointList.Add(RandomPoint(5, 5));
        }
        DrawPoints();

        float radius = 2;

        List<Vector3> circleList = GetAllCircles(radius);
        int index = 0;
        int[] counts = new int[circleList.Count];
        foreach (Vector3 circleCenter in circleList)
        {
            foreach (Vector3 point in pointList)
            {
                if (Vector3.Distance(point,circleCenter) <= radius)
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
                index = i;
            }
        }

        Vector3 maxCenter = circleList[index];
        Debug.Log(max + " points in circle");

        DrawCircle(maxCenter - new Vector3(0, 0, 0.5f), radius);
        //DrawCircleBlue(RecenterCircle(maxCenter, GetPointsInCircle(maxCenter,radius)) - new Vector3(0, 0, 0.5f), radius);
        DrawCircleBlue(new Vector3(0, 0, -0.5f), radius);
    }

    public Vector3 RandomPoint(float xRange, float yRange)
    {
        float zValue = 0;
        return (new Vector3(Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange), zValue));
    }

    public void DrawPoints()
    {
        if (pointObjects == null || pointObjects.Count < NumPoints)
        {
            pointObjects = new List<GameObject>();
            for (int i = 0; i < NumPoints; i++)
            {
                pointObjects.Add(Instantiate(prefabPoint, prefabPoint.transform.parent.transform));
                pointObjects[i].transform.localPosition = pointList[i];
                pointObjects[i].SetActive(true);
            }
            /*
            tempPoint = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint.SetActive(true);
            tempPoint.GetComponent<Image>().color = Color.blue;


            tempPoint2 = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint2.SetActive(true);
            tempPoint2.GetComponent<Image>().color = Color.blue;


            tempPoint3 = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint3.SetActive(true);
            tempPoint3.GetComponent<Image>().color = Color.blue;
            */
        }
        else
        {
            for (int i = 0; i < NumPoints; i++)
            {
                pointObjects[i].transform.localPosition = pointList[i];
                pointObjects[i].GetComponent<Image>().color = Color.black;

            }
        }
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        lineRenderer.transform.localPosition = center;

        int segments = 100;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;

        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    private void DrawCircleBlue(Vector3 center, float radius)
    {
        lineRendererBlue.transform.localPosition = center;

        int segments = 100;
        lineRendererBlue.positionCount = segments + 1;
        lineRendererBlue.useWorldSpace = false;

        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRendererBlue.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    private (int, int) GetPointPair(float maxDistance)
    {
        for (int i = 0; i < pointList.Count; i++)
        {
            for (int j = 0; j < pointList.Count; j++)
            {
                if (i != j && Vector3.Distance(pointList[i], pointList[j]) <= maxDistance)
                {
                    return (i, j);
                }
            }
        }
        return (0, 1);
    }

    private (Vector3, Vector3) GetCentersFromChord(Vector3 point1, Vector3 point2, float radius)
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

    private List<Vector3> GetAllCircles(float radius)
    {
        float diameter = radius * 2;
        List<Vector3> output = new List<Vector3>();

        for (int i = 0; i < pointList.Count; i++)
        {
            for (int j = 0; j < pointList.Count; j++)
            {
                if (i != j && Vector3.Distance(pointList[i], pointList[j]) <= diameter)
                {
                    (Vector3, Vector3) calcCenter = GetCentersFromChord(pointList[i], pointList[j], radius);
                    output.Add(calcCenter.Item1);
                    output.Add(calcCenter.Item2);
                }
            }
        }
        return output;
    }

    private List<Vector3> GetPointsInCircle(Vector3 center, float radius)
    {
        List<Vector3> output = new List<Vector3>();
        foreach (Vector3 point in pointList)
        {
            if (Vector3.Distance(point, center) <= radius)
            {
                output.Add(point);
            }
        }
        return (output);
    }

    private Vector3 RecenterCircle(Vector3 oldCenter, List<Vector3> points)
    {
        (Vector3, Vector3) max = (points[0], points[0]);
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; i < points.Count; i++)
            {
                if (i != j && Vector3.Distance(points[i],points[j]) < Vector3.Distance(max.Item1, max.Item2))
                {
                    max = (points[i], points[j]);
                }
            }
        }
        return (max.Item1 + max.Item2) / 2;
    }
}

