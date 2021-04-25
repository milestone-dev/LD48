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
    public SOCutscene Cutscene;
    public SOInventoryItem RequiredItemHeld;
    public bool ConsumeItem;
    public SOSwitch RequiredSwitch;
    public bool ClearSwitch;
}

public class SceneObjectController : MonoBehaviour {

    public string tooltipText;
    public SceneObjectType type;
    public SOInventoryItem Item;
    public bool ConsumeObject;
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
        {
            if (GameUIController.Instance.InventoryCanPickUpItem(Item)) { 
                GameUIController.Instance.InventoryAddItem(Item);
                if (ConsumeObject)
                {
                    GameController.Instance.CurrentSceneController.AddConsumedObject(gameObject);
                    Destroy(gameObject);
                }
            }
        }

        foreach(SceneObjectInteraction interaction in Interactions)
        {
            bool accept = true;
            if (interaction.RequiredSwitch && !GameController.Instance.IsSwitchSet(interaction.RequiredSwitch))
                accept = false;

            if (interaction.RequiredItemHeld && !GameUIController.Instance.InventoryIsHoldingItem(interaction.RequiredItemHeld))
                accept = false;

            if (accept)
            {
                if (interaction.ConsumeItem)
                {
                    GameUIController.Instance.InventoryRemoveItem(interaction.RequiredItemHeld);
                }
                if (interaction.ClearSwitch)
                {
                    GameController.Instance.ClearSwitch(interaction.RequiredSwitch);
                }
                GameUIController.Instance.CutsceneStart(interaction.Cutscene);
                return;
            }
        }

        GameController.Log("No acceptable interaction for", name);
    }
}






