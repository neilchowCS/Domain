using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutostartClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.singleton.StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
