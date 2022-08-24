using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayInstanceUI : MonoBehaviour
{
    public ReplayMessenger replayMessenger;
    public Image p1GridLayout;
    public Image p2GridLayout;

    public ReplayRecord replayRecord;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickReplay()
    {
        ReplayMessenger dontDestroy = Instantiate(replayMessenger);
        DontDestroyOnLoad(dontDestroy);

        dontDestroy.record = replayRecord;
        //Debug.Log(dontDestroy.GetType());
    }
}
