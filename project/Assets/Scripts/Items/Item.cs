﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public bool isDefault = false;
    public bool isEquipped = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

}