
using System;
using UnityEngine;


public class InventoryController : MonoBehaviour
{

    private Notification _notification;

    private InventoryModel _inventoryModel;
    private EquipmentController _equipmentController;

    private InventorySlot[] slots;  // inventory slots pełnią funkcije widoku
                                    

    #region Singleton
    public static InventoryController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryController !");
        }

        instance = this;
    }

    #endregion

    void Start()
    {
        _inventoryModel = InventoryModel.instance;
        _inventoryModel.onInventoryItemChangedCallback += UpdateUI;

        _equipmentController = GetComponentInChildren<EquipmentController>();
        slots = _inventoryModel.itemsParent.GetComponentsInChildren<InventorySlot>();

        _notification = Notification.instance;

        _inventoryModel.inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (_inventoryModel.inventoryUI.activeSelf)
            {
                _inventoryModel.inventoryUI.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                _inventoryModel.inventoryUI.SetActive(true);
                _inventoryModel.inventoryUI.SetActive(true);
            }

        }
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 1.0f;
            _inventoryModel.inventoryUI.SetActive(false);
        }

        if (_inventoryModel.inventoryUI.activeSelf)
            Time.timeScale = 0.0f;
        

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
        _inventoryModel.inventoryUI.SetActive(false);
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
        if (_notification.IsFree())
        {
            object[] tmp = new object[1];
            tmp[0] = inventorySlot.GetItem();
            _notification.ActiveYesNo((Action<Item>)_inventoryModel.RemoveInventoryItem, tmp, "Do you really want remove this item?");
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

    public bool ContainsItem(Item item)
    {
        return _inventoryModel.ContainsItem(item);
    }

    public void RemoveItem(Item item)
    {
        _inventoryModel.RemoveInventoryItem(item);
    }

    public int CountOfItem(string name)
    {
        return _inventoryModel.CountOfItem(name) + _equipmentController.CountOfItem(name);
    }
}