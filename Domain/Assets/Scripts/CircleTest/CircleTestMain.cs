using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTestMain : MonoBehaviour
{
    private List<Vector3> pointList;
    //Inspector
    public GameObject prefabPoint;
    private List<GameObject> pointObjects;
    private const int NumPoints = 12;

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
        ShowPoints();
    }

    public Vector3 RandomPoint(float xRange, float yRange)
    {
        float zValue = 0;
        return (new Vector3(Random.Range(-xRange, xRange),
            Random.Range(-yRange, yRange),zValue));
    }

    public void ShowPoints()
    {
        if (pointObjects == null||pointObjects.Count < NumPoints)
        {
            pointObjects = new List<GameObject>();
            for (int i = 0; i < NumPoints; i++)
            {
                pointObjects.Add(Instantiate(prefabPoint, prefabPoint.transform.parent.transform));
                pointObjects[i].transform.localPosition = pointList[i];
                pointObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < NumPoints; i++)
            {
                pointObjects[i].transform.localPosition = pointList[i];
            }
        }
    }
}

