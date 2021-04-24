using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    InCutscene,
    InteractingWithToolbars,
    NavigatingScene
}

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public SceneController CurrentSceneController;
    public static GameState State = GameState.NavigatingScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public static void CutsceneHandleCallback(string info)
    {
        if (GameController.Instance && GameController.Instance.CurrentSceneController)
        {
            GameController.Instance.CurrentSceneController.CutsceneHandleCallback(info);
        }
    }

}

