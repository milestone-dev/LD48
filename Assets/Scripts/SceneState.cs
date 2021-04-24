using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

