using UnityEngine;
using System.Collections;

public class SubmarineEngineRoomSceneController : SceneController
{
    public SubmarineEngineRoomSceneState State;

    public override void EnterScene()
    {
        base.EnterScene();
        State = FindObjectOfType<SubmarineEngineRoomSceneState>();
        DestroyConsumedObjectNames(State);
    }

    public override void AddConsumedObject(GameObject obj)
    {
        State.AddConsumedObject(obj);
    }

}
