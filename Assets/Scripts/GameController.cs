using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public static void Instantiate()
    {
        if (!GameController.Instance)
        {
            Instantiate(Resources.Load<GameController>("Prefabs/GameController"));
        }
    }

    private void Start()
    {
        if (GameController.Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        State = GetComponent<GameState>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnSceneChanged;
        CurrentSceneController = FindObjectOfType<SceneController>();
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        GameController.Log("Scene Changed", currentScene, nextScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameController.Log("Scene Loaded", scene);

        CurrentSceneController = FindObjectOfType<SceneController>();
        Debug.Log("Scene Controller" + CurrentSceneController);
    }

    public void TransitionToScene(string sceneName)
    {
        // TODO Add fading coroutines yay
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public static void CutsceneHandleCallback(string info)
    {
        if (GameController.Instance && GameController.Instance.CurrentSceneController)
        {
            GameController.Instance.CurrentSceneController.CutsceneHandleCallback(info);
        }
    }

    public static void Log(params object[] messages)
    {
        Debug.Log(string.Join(" ", messages));
    }

}

