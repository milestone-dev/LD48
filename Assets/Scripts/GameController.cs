using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameInteractionState
{
    InCutscene,
    InteractingWithToolbars,
    NavigatingScene
}

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public SceneController CurrentSceneController;
    public GameState State;
    public static GameInteractionState InteractionState = GameInteractionState.NavigatingScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        State = GetComponent<GameState>();
    }

    public static void CutsceneHandleCallback(string info)
    {
        if (GameController.Instance && GameController.Instance.CurrentSceneController)
        {
            GameController.Instance.CurrentSceneController.CutsceneHandleCallback(info);
        }
    }

}

