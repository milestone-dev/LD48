using UnityEngine;
using System.Collections;

public class SubmarineLivingQuartersSceneState : SceneState
{
    private void Awake()
    {
        if (FindObjectsOfType<SubmarineLivingQuartersSceneState>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
