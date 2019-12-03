
using System;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    private Notification notification;

    InventoryModel inventoryModel;
    EquipmentModel equipmentModel;

    InventorySlot[] slots;  // inventory slots pełnią funkcije widoku
    // Start is called before the first frame update
    void Start()
    {
        inventoryModel = InventoryModel.instance;
        inventoryModel.onInventoryItemChangedCallback += UpdateUI;

        equipmentModel = EquipmentModel.instance;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        var tmp = GameObject.FindGameObjectWithTag("Notification");
;
        if (tmp != null)
            notification = tmp.GetComponent(typeof(Notification)) as Notification;
        else
            Debug.LogError("Brak tagu notification");

        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            inventoryUI.SetActive(false);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventoryModel.inventoryItems.Count)
            {
                slots[i].AddItem(inventoryModel.inventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].RemoveDescription();
            }

        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void UseItem(InventorySlot inventorySlot)
    {
        Item item = inventorySlot.GetItem();
        if (item != null)
        {
            if (item.GetType() == typeof(Armor))
            {
                Armor oldArmor =equipmentModel.AddArmor((Armor)item);
                inventoryModel.RemoveInventoryItem(item);
                if(oldArmor != null)
                    inventoryModel.AddInventoryItem(oldArmor);
            }
            else if (item.GetType() == typeof(Weapon))
            {
                item.Use();
                Weapon oldWeapon = equipmentModel.AddWeapon((Weapon)item);
                inventoryModel.RemoveInventoryItem(item);
                if (oldWeapon != null)
                {
                    inventoryModel.AddInventoryItem(oldWeapon);
                    oldWeapon.UnEquip();
                }
            }
            else
                inventorySlot.UseItem();

            inventorySlot.description.SetActive(false);

        }
    }

    public void OnRemoveButton(InventorySlot inventorySlot)
    {
        if (notification.IsFree())
        {
            notification.SetText("Do you really want remove this item?");
            object[] tmp = new object[1];
            tmp[0] = inventorySlot.GetItem();
            notification.ActiveYesNo((Action<Item>)inventoryModel.RemoveInventoryItem, tmp);
        }
    }

}