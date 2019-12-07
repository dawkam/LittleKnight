using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootModel : MonoBehaviour
{
    #region Singleton
    public static LootModel instance;
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

    public delegate void OnLootItemChanged();
    public OnLootItemChanged onLootItemChangedCallback;

    public List<List<Item>> waitingItems;           // wykorzystywane przy np. otwarciu wielu skrzynek naraz. Są odczytywane po kolei od pozycji 0 po odczytaniu pozycja 0 jest usuwana i leci dalej   
    public int lootSize;

    #region waitingsItem
    public void AddWaitingItems(ref List<Item> Items)
    {

        waitingItems.Add(Items);
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }

    public void TakeAllItems()
    {
        waitingItems[0].Clear();
        waitingItems.RemoveAt(0);
        if (onLootItemChangedCallback != null)
            onLootItemChangedCallback.Invoke();
    }
    public void RemoveWaitingItems()
    {
        waitingItems.RemoveAt(0);
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

}
