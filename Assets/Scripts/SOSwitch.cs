using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Switch", fileName = "Switch")]

public class SOSwitch : ScriptableObject
{
    public static SOSwitch Load(string name)
    {
        return Resources.Load<SOSwitch>("Conditions/" + name);
    }
}

