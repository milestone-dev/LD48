using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character", fileName = "Character")]

public class SOCharacter : ScriptableObject
{
    public Sprite avatar;
    public Color textColor;
    public AnimationClip IdleAnimationClip;
    public AnimationClip TalkAnimationClip;
}

