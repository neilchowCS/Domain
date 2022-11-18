using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerators : MonoBehaviour
{
    public int s;
    public System.Random rand0;
    public System.Random rand1;

    private void Awake()
    {
        s = (int)System.DateTime.Now.Ticks & 0x0000FFFF;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewRandom()
    {
        s++;
        rand0 = new System.Random(s);
        rand1 = new System.Random(s);
    }

    public void RestoreRandom(int seed)
    {
        rand0 = new System.Random(seed);
        rand1 = new System.Random(seed);
    }
}
