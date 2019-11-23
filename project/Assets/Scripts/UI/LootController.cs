using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject lootUI;

    LootModel lootModel;
    InventoryModel inventoryModel;

    LootSlot[] slots; // inventory slots pełnią funkcije widoku
    // Start is called before the first frame update
    void Start()
    {
        lootModel = LootModel.instance;
        lootModel.onLootItemChangedCallback += UpdateUI;

        inventoryModel = InventoryModel.instance;
        slots = itemsParent.GetComponentsInChildren<LootSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lootModel.waitingItems.Count != 0)
        {
            lootUI.SetActive(true);
        }
        else if (lootModel.waitingItems.Count == 0)
        {
            lootUI.SetActive(false);
        }

    }

    void UpdateUI()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if(lootModel.waitingItems.Count != 0 && i < lootModel.waitingItems[0].Count)
            {
                slots[i].AddItem(lootModel.waitingItems[0][i]);
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].RemoveDescription();
            }

        }
       // lootSystem.waitingItems.RemoveAt(0);
    }

    public void TakeAllItemsFromWaitingList()
    {
        if (inventoryModel.AddInventoryItems(lootModel.waitingItems[0]))
        {
            lootModel.RemoveWaitingItems();
        }else
        {
            //alert
        }
    }

    public void CloseLoot()
    {
        lootModel.RemoveWaitingItems();
    }

    public void AddInventoryItem(Item item)
    {
        if (inventoryModel.AddInventoryItem(item)) //próba dodania do inventory
        {
            lootModel.RemoveWaitingItem(item); //usuwanie z okna lootu 

        }
        else
        {
            Debug.LogWarning("Alert do dodania"); //alert

        }

    }
    public void UseItem(LootSlot lootSlot)
    {
        Item item = lootSlot.GetItem();
        if ( item != null)
        {
            AddInventoryItem(item);
        }
    }
}
