using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayHandler : MonoBehaviour
{
    public CollectionHandler collectionHandler;
    public BaseUnitIcon prefabIcon;
    public ReplayInstanceUI prefabReplayInstance;
    public GameObject scrollContents;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Top left: -114.5, 65
            lower: negative y
            side: 0.5

            Delta x = 115
        */
        float deltaX = 115;

        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(-114.5f, 65, 0);
            ReplayInstanceUI ui = Instantiate(prefabReplayInstance, scrollContents.transform);
            for (int j = 0; j < 6; j++)
            {
                BaseUnitIcon icon = Instantiate(prefabIcon, ui.p1GridLayout.transform);
                icon.transform.localPosition = position;
                icon.transform.localScale = new Vector3(0.527f, 0.527f, 0.527f);

                position += new Vector3(deltaX, 0, 0);
                if (j == 2)
                {
                    position = new Vector3(position.x - deltaX*3, -position.y, 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
