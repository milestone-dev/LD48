﻿using UnityEngine;
using System.Collections;

public class SubmarineLivingQuartersSceneController : SceneController
{
    public SubmarineLivingQuartersSceneState State;

    public override void EnterScene()
    {
        base.EnterScene();
        State = FindObjectOfType<SubmarineLivingQuartersSceneState>();
        DestroyConsumedObjectNames(State);
    }

    public override void AddConsumedObject(GameObject obj)
    {
        State.AddConsumedObject(obj);
    }
}
