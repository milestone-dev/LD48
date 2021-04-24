using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogTreeOption
{
    public string Text;
    public bool DisabledByDefault;
    public SOCutscene Cutscene;
    public SODialogTree DialogTree;
    public string tag;
}

[CreateAssetMenu(menuName = "New Dialog Tree", fileName = "Dialog Tree")]

public class SODialogTree : ScriptableObject
{
    public DialogTreeOption[] Options;
    public SOCharacter Character;
}

