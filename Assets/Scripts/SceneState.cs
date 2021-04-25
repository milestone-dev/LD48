using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour {

    public float cameraX;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}