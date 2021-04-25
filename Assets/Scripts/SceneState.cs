using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour {

    public float cameraX;
    public List<string> DestroyedObjectNames = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}