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
    private const int NumPoints = 12;

    public LineRenderer lineRenderer;

    void Start()
    {
        Execute();
    }

    public void Execute()
    {
        pointList = new List<Vector3>();
        for (int i = 0; i < NumPoints; i++)
        {
            pointList.Add(RandomPoint(5, 5));
        }
        DrawPoints();

        float radius = 4;

        (int, int) tuple = GetPointPair(radius);
        Vector3 point1 = pointList[tuple.Item1];
        Vector3 point2 = pointList[tuple.Item2];

        pointObjects[tuple.Item1].GetComponent<Image>().color = Color.red;
        pointObjects[tuple.Item2].GetComponent<Image>().color = Color.red;

        Vector3 midPoint = (point1 + point2)/2;
        tempPoint.transform.localPosition = midPoint;

        DrawCircle(midPoint - new Vector3(0, 0, 0.5f), radius);
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
            tempPoint = Instantiate(prefabPoint, prefabPoint.transform.parent.transform);
            tempPoint.SetActive(true);
            tempPoint.GetComponent<Image>().color = Color.blue;
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

