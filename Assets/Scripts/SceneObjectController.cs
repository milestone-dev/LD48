using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SceneObjectType
{
    InteractableObject,
    ObservableObject,
    Character,
    Exit
}

[Serializable]
public struct SceneObjectInteraction
{
    public SOSwitch RequiredSwitch;
    public SOCutscene Cutscene;
}

public class SceneObjectController : MonoBehaviour {

    public string tooltipText;
    public SceneObjectType type;
    public SOInventoryItem Item;
    public List<SceneObjectInteraction> Interactions;

    private void OnMouseEnter()
    {
        if (GameController.InteractionState != GameInteractionState.NavigatingScene)
            return;

        GameUIController.Instance.SceneObjectMouseEnter(this);
    }

    private void OnMouseExit()
    {
        if (GameController.InteractionState != GameInteractionState.NavigatingScene)
            return;

        GameUIController.Instance.SceneObjectMouseExit(this);
    }

    private void OnMouseUp()
    {
        if (GameController.InteractionState != GameInteractionState.NavigatingScene)
            return;

        if (GameController.InteractionState == GameInteractionState.NavigatingScene && Item)
            GameUIController.Instance.InventoryAddItem(Item);

        foreach(SceneObjectInteraction interaction in Interactions)
        {
            if (!interaction.RequiredSwitch || GameController.Instance.IsSwitchSet(interaction.RequiredSwitch)) {
                GameUIController.Instance.CutsceneStart(interaction.Cutscene);
                break;
            }
        }
    }
}






