using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    #region Singleton
    public static LootSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Lootsystem !");
        }

        instance = this;
        waitingItems = new List<List<Item>>();
    }

    #endregion

    public delegate void OnInventoryItemChanged();
    public OnInventoryItemChanged onInventoryItemChangedCallback;


    public delegate void OnLootItemChanged();
    public OnLootItemChanged onLootItemChangedCallback;

    public List<List<Item>> waitingItems;           // wykorzystywane przy np. otwarciu wielu skrzynek naraz. Są odczytywane po kolei od pozycji 0 po odczytaniu pozycja 0 jest usuwana i leci dalej   

    public List<Item> inventoryItems;

    public int inventorySize;
    public int lootSize;

    #region waitingsItem
    public void AddWaitingItems(List<Item> Items)
    {

        waitingItems.Add(Items);
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }

    public void RemoveWaitingItems()
    {
        waitingItems.RemoveAt(0);
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }

    public void RemoveWaitingItems(List<Item> items)
    {
        waitingItems.Remove(items);
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }

    public void RemoveWaitingItem(Item item)
    {
        waitingItems[0].Remove(item);
        if (waitingItems[0].Count == 0)
            RemoveWaitingItems();
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }

    #endregion

    #region Inventory
    public bool AddInventoryItem(Item item)
    {
        if (inventoryItems.Count == inventorySize)
        {
            Debug.LogWarning("Full inventory!");
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
    public void RemoveInventoryItems(Item item)
    {
        inventoryItems.Remove(item);
        if (onInventoryItemChangedCallback != null)
            onInventoryItemChangedCallback.Invoke();
    }
    #endregion
}
