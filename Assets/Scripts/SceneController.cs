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

    public virtual void EnterScene()
    {
    }

    public virtual void ExitToScene(string sceneName)
    {
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

