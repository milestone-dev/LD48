using UnityEngine;
using System.Collections;

public class SubmarineBridgeSceneController : SceneController
{
    public SubmarineBridgeSceneState State;
    public GameObject CoffeeCup;


    public override void EnterScene()
    {
        base.EnterScene();
        State = FindObjectOfType<SubmarineBridgeSceneState>();

        if (State.PickedUpCoffeeCup)
        {
            Destroy(CoffeeCup);
        }

        DestroyConsumedObjectNames(State);
    }

    public override void AddConsumedObject(GameObject obj)
    {
        State.AddConsumedObject(obj);
    }

    public override void CutsceneHandleCallback(string tag)
    {
        if (tag.Equals("RemoveCoffeeCup"))
        {
            Destroy(CoffeeCup);
            State.PickedUpCoffeeCup = true;
            return;
        }

        if (tag.Equals("Descend"))
        {
            ExitToScene("Act2Scene");
            return;
        }
    }
}
