using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private void Start()
    {
        GameController.Instantiate();
        EnterScene();
    }

    private void Update()
    {
        if (!GameController.Instance)
        {
            GameController.Instantiate();
        }
    }

    public virtual void EnterScene()
    {
        // Nothing should happen here since we don't know the state.
    }

    public virtual void AddConsumedObject(GameObject obj)
    {
        GameController.LogError("AddConsumedObject called on SceneController");
    }

    public void DestroyConsumedObjectNames(SceneState state)
    {
        foreach (string name in state.DestroyedObjectNames)
        {
            GameObject obj = GameObject.Find(name);
            if (obj)
            {
                Destroy(obj);
            } else
            {
                GameController.LogError("FAILED Destroying", name);
            }
        }
    }

    public virtual void ExitToScene(string sceneName)
    {
        if (GameController.InteractionState != GameInteractionState.NavigatingScene)
            return;

        GameController.Instance.TransitionToScene(sceneName);
    }

    public virtual void CutsceneHandleCallback(string tag)
    {
        GameController.Log("Unhandled CutsceneCallback", tag);
    }

    public virtual void AnimationCallback(GameObject obj)
    {
        GameController.Log("AnimationCallback", obj);
    }

    public void AddInventoryObject(string inventoryObjectName)
    {
        GameUIController.Instance.InventoryAddItem(inventoryObjectName);
    }

    public void RemoveInventoryObject(string inventoryObjectName)
    {
        GameUIController.Instance.InventoryRemoveItem(inventoryObjectName);
    }

    public void PlayAudioClip(string audioClipName)
    {
        GameAudioPlayerController.Instance.PlayAudioClipNamed(audioClipName);
    }

    public void PlayMusicClip(string musicAudioClipName)
    {
        GameMusicPlayerControler.Instance.PlayMusicNamed(musicAudioClipName);
    }

    public bool IsHoldingInventoryObject(string inventoryObjectName)
    {
        return GameUIController.Instance.InventoryIsHoldingItemNamed(inventoryObjectName);
    }

    public void StartCutscene(string cutsceneName)
    {
        GameUIController.Instance.CutsceneStart(SOCutscene.Load(cutsceneName));
    }

    public void StartCutscene(SOCutscene cutscene)
    {
        GameUIController.Instance.CutsceneStart(cutscene);
    }

}

