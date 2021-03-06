﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    //public bool isDefault = false;
    public string description;
    public virtual void Use()
    {
        //Debug.Log("Using " + name);
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public virtual string GetName()
    {
        return name;
    }
    public virtual void UnEquip()
    {
        
    }

}
