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
    public SOInventoryItem Item;

    private void OnMouseEnter()
    {
        if (GameController.InteractionState == GameInteractionState.NavigatingScene)
            GameUIController.Instance.SceneObjectMouseEnter(this);
    }

    private void OnMouseExit()
    {
        if (GameController.InteractionState == GameInteractionState.NavigatingScene)
            GameUIController.Instance.SceneObjectMouseExit(this);
    }

    private void OnMouseUp()
    {
        if (GameController.InteractionState == GameInteractionState.NavigatingScene && Item)
            GameUIController.Instance.InventoryAddItem(Item);

        if (GameController.InteractionState == GameInteractionState.NavigatingScene && Cutscene)
            GameUIController.Instance.CutsceneStart(Cutscene);

    }
}






