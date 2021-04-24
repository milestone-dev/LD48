using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneObjectType
{
    InteractableObject,
    ObservableObject,
    Character,
    Exit
}

public class SceneObjectController : MonoBehaviour {

    public string tooltipText;
    public SceneObjectType type;
    public SOCutscene Cutscene;

    private void OnMouseEnter()
    {
        if (GameController.State == GameState.NavigatingScene)
            GameUIController.Instance.SceneObjectMouseEnter(this);
    }

    private void OnMouseExit()
    {
        if (GameController.State == GameState.NavigatingScene)
            GameUIController.Instance.SceneObjectMouseExit(this);
    }

    private void OnMouseUp()
    {
        if (GameController.State == GameState.NavigatingScene && Cutscene)
            GameUIController.Instance.CutsceneStart(Cutscene);
    }
}






