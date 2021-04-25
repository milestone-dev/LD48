using UnityEngine;
using System.Collections;

public class SubmarineHallwaySceneController : SceneController
{
    public SubmarineHallwaySceneState State;

    public override void EnterScene()
    {
        base.EnterScene();
        State = FindObjectOfType<SubmarineHallwaySceneState>();
        DestroyConsumedObjectNames(State);
    }

    public override void AddConsumedObject(GameObject obj)
    {
        State.AddConsumedObject(obj);
    }
}
