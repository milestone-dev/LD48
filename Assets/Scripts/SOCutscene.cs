using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct CutsceneEntry
{
    public SOCharacter Character;
    [Multiline]
    public string Text;
    public AudioClip AudioClip;
    public float ExtraDuration;
    public UnityEvent Callback;
}

[CreateAssetMenu(menuName = "New Cutscene", fileName = "Cutscene")]
public class SOCutscene : ScriptableObject
{
    public CutsceneEntry[] Entries;
    public SODialogTree DialogTree;

    public static SOCutscene Load(string name)
    {
        return Resources.Load<SOCutscene>("Cutscenes/" + name);
    }
}


