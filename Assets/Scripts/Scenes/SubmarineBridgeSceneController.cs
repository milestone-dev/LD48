using UnityEngine;
using System.Collections;

public class SubmarineBridgeSceneController : SceneController
{
    public void InteractWithCaptain()
    {
        if (GameController.InteractionState != GameInteractionState.NavigatingScene)
            return;

        if (!GameController.Instance.IsSwitchSet("CaptainIntroDone"))
        {
            StartCutscene("Submarine_Captain_Intro");
        } else
        {
            StartCutscene("Submarine_Captain_Default");

        }
    }
}
