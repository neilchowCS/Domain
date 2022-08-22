using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBattle : MonoBehaviour
{
    public BattleExecutor executor;
    // Start is called before the first frame update
    void Start()
    {
        foreach (BattleExecutor exec in FindObjectsOfType<BattleExecutor>())
        {
            executor = exec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Repeat()
    {
        executor.ExecuteBattle();
    }
}
