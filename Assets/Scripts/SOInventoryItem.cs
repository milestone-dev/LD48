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
        SOInventoryItem item = Resources.Load<SOInventoryItem>("Items/" + name);
        if (!item)
        {
            Debug.LogError("SOInventoryItem failed:" + name);
        }
        return item;
    }
}

