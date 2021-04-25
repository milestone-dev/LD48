using UnityEngine;
using System.Collections;

public class SubmarineBridgeSceneState : SceneState
{
    public bool PickedUpCoffeeCup;

    private void Awake()
    {
        if (FindObjectsOfType<SubmarineBridgeSceneState>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
