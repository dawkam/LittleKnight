
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    LootSystem lootSystem;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        lootSystem = LootSystem.instance;
        lootSystem.onInventoryItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < lootSystem.inventoryItems.Count)
            {
                slots[i].AddItem(lootSystem.inventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

}
