using UnityEngine;
using System.Collections;

public class SubmarineLabSceneState : SceneState
{
    private void Awake()
    {
        if (FindObjectsOfType<SubmarineLabSceneState>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
