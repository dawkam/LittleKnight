﻿
using System;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    private Notification notification;

    private InventoryModel _inventoryModel;
    private EquipmentController _equipmentController;

    private InventorySlot[] slots;  // inventory slots pełnią funkcije widoku
    // Start is called before the first frame update
    void Start()
    {
        _inventoryModel = InventoryModel.instance;
        _inventoryModel.onInventoryItemChangedCallback += UpdateUI;

        _equipmentController = GetComponentInChildren<EquipmentController>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        notification = Notification.instance;

        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale =  0.0f;
                inventoryUI.SetActive(true);
            }

        }
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 1.0f;
            inventoryUI.SetActive(false);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < _inventoryModel.inventoryItems.Count)
            {
                slots[i].AddItem(_inventoryModel.inventoryItems[i]);
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
                Armor oldArmor =_equipmentController.AddArmor((Armor)item);
                _inventoryModel.RemoveInventoryItem(item);
                if(oldArmor != null)
                    _inventoryModel.AddInventoryItem(oldArmor);
            }
            else if (item.GetType() == typeof(Weapon))
            {
                item.Use();
                Weapon oldWeapon = _equipmentController.AddWeapon((Weapon)item);
                _inventoryModel.RemoveInventoryItem(item);
                if (oldWeapon != null)
                {
                    _inventoryModel.AddInventoryItem(oldWeapon);
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
            notification.ActiveYesNo((Action<Item>)_inventoryModel.RemoveInventoryItem, tmp);
        }
    }

    public int GetInventorySize()
    {
        return _inventoryModel.inventorySize;
    }

    public int GetItemsCount()
    {
        return _inventoryModel.inventoryItems.Count;
    }

    public void AddInventoryItem(Item item)
    {
        _inventoryModel.AddInventoryItem(item);
    }

}