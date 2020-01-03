using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryModel : MonoBehaviour
{
    #region Singleton
    public static InventoryModel instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Lootsystem !");
        }

        instance = this;
    }

    #endregion

    public delegate void OnInventoryItemChanged();
    public OnInventoryItemChanged onInventoryItemChangedCallback;

    public List<Item> inventoryItems;

    public int inventorySize;

    #region Inventory
    public bool AddInventoryItem(Item item)
    {
        if (inventoryItems.Count == inventorySize)
        {
            //Debug.LogWarning("Full inventory!");
            return false;
        }

        inventoryItems.Add(item);
        if (onInventoryItemChangedCallback != null)
            onInventoryItemChangedCallback.Invoke();

        return true;
    }

    public bool AddInventoryItems(List<Item> items)
    {
        if (inventoryItems.Count + items.Count > inventorySize)
        {
            Debug.LogWarning("Full inventory");
            return false;
        }


            inventoryItems.AddRange(items);
            
        if (onInventoryItemChangedCallback != null)
            onInventoryItemChangedCallback.Invoke();
        return true;
    }
    public void RemoveInventoryItem(Item item)
    {
        inventoryItems.Remove(item);
        if (onInventoryItemChangedCallback != null)
            onInventoryItemChangedCallback.Invoke();
    }

    public bool ContainsItem(Item item)
    {
        return inventoryItems.Contains(item);
    }

    public int CountOfItem(string item)
    {
        return inventoryItems.Where(x => x.name == item).Count();
    }
    #endregion
}
