using UnityEngine;
using System.Collections;

public class SubmarineBridgeSceneController : SceneController
{
    public void InteractWithCaptain()
    {
        if (!GameController.Instance.State.CaptainIntroComplete)
        {
            StartCutscene("Submarine_Captain_Intro");
            GameController.Instance.State.CaptainIntroComplete = true;
        } else
        {
            StartCutscene("Submarine_Captain_Default");

        }
    }
}
