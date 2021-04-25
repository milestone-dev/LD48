using UnityEngine;
using System.Collections;

public class SubmarineEngineRoomSceneState : SceneState
{
    private void Awake()
    {
        if (FindObjectsOfType<SubmarineEngineRoomSceneState>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
