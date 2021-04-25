using UnityEngine;
using System.Collections;

public class SubmarineHallwaySceneState: SceneState

{
    private void Awake()
    {
        if (FindObjectsOfType<SubmarineHallwaySceneState>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

}
