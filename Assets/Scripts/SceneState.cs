using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour {

    public float cameraX;
    public List<string> DestroyedObjectNames = new List<string>();

    public void AddConsumedObject(GameObject obj)
    {
        GameController.Log("----- Add to DestroyedObjectNames", obj.name);
        DestroyedObjectNames.Add(obj.name);
    }
}