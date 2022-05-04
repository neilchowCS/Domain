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

    private const int NumPoints = 12;

    public LineRenderer lineRenderer;

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
        float diameter = 2 * radius;

        (int, int) tuple = GetPointPair(diameter);
        Vector3 point1 = pointList[tuple.Item1];
        Vector3 point2 = pointList[tuple.Item2];

        pointObjects[tuple.Item1].GetComponent<Image>().color = Color.red;
        pointObjects[tuple.Item2].GetComponent<Image>().color = Color.red;
        pointObjects[tuple.Item1].SetActive(true);
        pointObjects[tuple.Item2].SetActive(true);

        Vector3 midPoint = (point1 + point2)/2;
        tempPoint.transform.localPosition = midPoint;

        //chord = point1 - point2
        float temp = Vector3.Distance(point1, point2)/2;
        float chordMultiple = temp * temp;

        Debug.Log("intersect: " + temp);
        Debug.Log("chordMultiple: " + chordMultiple);
        //chord = (diameter - x)x = - x^2 + (diameter)x
        float positiveSolution = 0;

        float a = -1;
        float b = diameter;
        float c = -chordMultiple;

        float determinant = (b * b) - (4 * a * c);
        if (determinant >= 0)
        {
            float solution1 = (-b + Mathf.Sqrt(determinant)) / (2 * a);
            float solution2 = (-b - Mathf.Sqrt(determinant)) / (2 * a);

            Debug.Log(solution1);
            Debug.Log(solution2);
            
            if (solution1 < diameter && solution1 > 0)
            {
                positiveSolution = solution1;
            }else if (solution2 < diameter && solution2 > 0)
            {
                positiveSolution = solution2;
            }
        }

        Vector3 direction = point1 - point2;
        Vector3 perp = Vector3.Cross(direction, new Vector3(0,0,1));

        Vector3 border = midPoint + (perp.normalized * positiveSolution);
        tempPoint2.transform.localPosition = border;

        Vector3 calcCenter = midPoint - (perp.normalized * (radius - positiveSolution));
        tempPoint3.transform.localPosition = calcCenter;

        rayDirection = perp.normalized * radius * scaleFactor;
        rayOrigin = tempPoint3.transform.position;

        DrawCircle(calcCenter - new Vector3(0, 0, 0.5f), radius);
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
                //pointObjects[i].SetActive(true);
            }
            tempPoint = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint.SetActive(true);
            tempPoint.GetComponent<Image>().color = Color.blue;


            tempPoint2 = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint2.SetActive(true);
            tempPoint2.GetComponent<Image>().color = Color.blue;


            tempPoint3 = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint3.SetActive(true);
            tempPoint3.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            for (int i = 0; i < NumPoints; i++)
            {
                pointObjects[i].transform.localPosition = pointList[i];
                pointObjects[i].GetComponent<Image>().color = Color.black;
                pointObjects[i].SetActive(false);

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
}

