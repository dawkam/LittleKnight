using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject lootUI;

    LootSystem lootSystem;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        lootSystem = LootSystem.instance;
        lootSystem.onLootItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lootSystem.waitingItems.Count != 0)
        {
            lootUI.SetActive(true);
        }
        else if (lootSystem.waitingItems.Count == 0)
        {
            lootUI.SetActive(false);
        }

    }

    void UpdateUI()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if(lootSystem.waitingItems.Count != 0 && i < lootSystem.waitingItems[0].Count)
            {
                slots[i].AddItem(lootSystem.waitingItems[0][i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }
       // lootSystem.waitingItems.RemoveAt(0);
    }

    public void TakeAllItemsFromWaitingList()
    {
        if (lootSystem.AddInventoryItems(lootSystem.waitingItems[0]))
        {
            lootSystem.RemoveWaitingItems();
        }else
        {
            //alert
        }
    }

    public void CloseLoot()
    {
        lootSystem.RemoveWaitingItems();
    }
}
