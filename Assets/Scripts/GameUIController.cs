using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUIController : MonoBehaviour {

    public static GameUIController Instance;
    private void Awake()
    {
        Instance = this;
    }
}

