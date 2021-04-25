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
    public List<SOSwitch> Switches;
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
        GameController.Log("GameController Instantiated.");
    }

    private void Update()
    {
        // TODO: A bit of a hack but totally fine for now

        if (!IsSwitchSet("ComputerKnowPassword") &&
            IsSwitchSet("CaptainKnowPasswordHint") &&
            IsSwitchSet("EngineerKnowPasswordHint") &&
            IsSwitchSet("ResearcherKnowPasswordHint"))
        {
            SetSwitch("ComputerKnowPassword");
        }

        if (!IsSwitchSet("EveryoneReady") &&
            IsSwitchSet("EngineerReady") &&
            IsSwitchSet("ResearcherReady") &&
            IsSwitchSet("SonarReady"))
        {
            SetSwitch("EveryoneReady");
        }
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        GameController.Log("Scene Changed!", currentScene, nextScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameController.Log("Scene Loaded", scene);

        CurrentSceneController = FindObjectOfType<SceneController>();
        Debug.Log("Scene Controller" + CurrentSceneController);
    }

    public static void GoToScene(string sceneName)
    {
        // TODO Add fading coroutines yay
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void TransitionToScene(string sceneName)
    {
        // TODO Add fading coroutines yay
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public static void CutsceneHandleCallback(string tag)
    {
        GameController.Log("CutsceneHandleCallback", tag, GameController.Instance.CurrentSceneController);
        if (GameController.Instance && GameController.Instance.CurrentSceneController)
        {
            GameController.Instance.CurrentSceneController.CutsceneHandleCallback(tag);
        }
    }

    public static void Log(params object[] messages)
    {
        Debug.Log(string.Join(" ", messages));
    }

    public static void LogError(params object[] messages)
    {
        Debug.LogError(string.Join(" ", messages));
    }

    public bool IsSwitchSet(string switchName)
    {
        return Switches.Exists(flag => flag.name.Equals(switchName));
    }

    public bool IsSwitchSet(SOSwitch sw)
    {
        return Switches.Contains(sw);
    }

    public void SetSwitch(string switchName)
    {
        if (!Switches.Exists(sw => sw.name.Equals(switchName))) {
            Switches.Add(SOSwitch.Load(switchName));
        }
    }
    public void SetSwitch(SOSwitch sw)
    {
        if (!Switches.Contains(sw))
        {
            Switches.Add(sw);
        }
    }

    public void ClearSwitch(string switchName)
    {
        Switches.RemoveAll(sw => sw.name.Equals(switchName));
    }

    public void ClearSwitch(SOSwitch sw)
    {
        Switches.Remove(sw);
    }

}

