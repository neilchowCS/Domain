using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissalIcon : BaseUnitIcon
{
    public DismissalManager dismissalManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseUnit()
    {
        dismissalManager.ChosenUnit(this);
    }
}
