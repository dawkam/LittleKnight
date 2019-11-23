
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

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
            if (item.GetType() == new Armor().GetType())
            {
                Armor oldArmor =equipmentModel.AddArmor((Armor)item);
                inventoryModel.RemoveInventoryItem(item);
                if(oldArmor != null)
                    inventoryModel.AddInventoryItem(oldArmor);
            }
            else if (item.GetType() == new Weapon().GetType())
            {
                Weapon oldWeapon = equipmentModel.AddWeapon((Weapon)item);
                inventoryModel.RemoveInventoryItem(item);
                if(oldWeapon != null)
                    inventoryModel.AddInventoryItem(oldWeapon);
            }
            else
                inventorySlot.UseItem();

            inventorySlot.description.SetActive(false);

        }
    }

    public void OnRemoveButton(InventorySlot inventorySlot)
    {
        Debug.LogWarning("alert do zrobienia");
        inventoryModel.RemoveInventoryItem(inventorySlot.GetItem());
    }

}