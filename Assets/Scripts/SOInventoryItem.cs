using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Inventory Object", fileName = "Inventory Object")]
public class SOInventoryItem : ScriptableObject
{
    public string description;
    public Sprite icon;

    public static SOInventoryItem Load(string name)
    {
        return Resources.Load<SOInventoryItem>("Items/" + name);
    }
}

