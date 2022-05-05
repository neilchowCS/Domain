using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioSO", order = 1)]
public class AudioSingleton : ScriptableObject
{
    [SerializeField]
    private static BackgroundAudio prefabAudio;
    public static BackgroundAudio PrefabAudio {
        get
        {
            if (prefabAudio == null)
            {
                prefabAudio = Resources.Load<BackgroundAudio>("BackgroundMusic");
            }
            return prefabAudio;
        }
    }
    public static BackgroundAudio Instance
    {
        get; set;
    }

}
