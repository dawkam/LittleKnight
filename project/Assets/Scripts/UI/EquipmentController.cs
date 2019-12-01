using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Notification;

public class EquipmentController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject equipmentUI;

    private Notification notification;

    EquipmentModel equipmentModel;
    InventoryModel inventoryModel;

    EquipmentSlot[] slots; // inventory slots pełnią funkcije widoku, 0 - hełm, 1 - tors, 2 - buty, 3 - broń

    private void Start()
    {
        equipmentModel = EquipmentModel.instance;
        equipmentModel.onEquipmentItemChangedCallback += UpdateUI;

        inventoryModel = InventoryModel.instance;
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        var tmp = GameObject.FindGameObjectWithTag("Notification");
        if (tmp != null)
            notification = tmp.GetComponent(typeof(Notification)) as Notification;
        else
            Debug.LogError("Brak tagu notification");
    }

    void UpdateUI()
    {
        if (equipmentModel.armorList[0] != null)
        {
            slots[0].AddItem(equipmentModel.armorList[0]);

        }
        else
            slots[0].ClearSlot();

        if (equipmentModel.armorList[1] != null)
            slots[1].AddItem(equipmentModel.armorList[1]);
        else
            slots[1].ClearSlot();

        if (equipmentModel.armorList[2] != null)
            slots[2].AddItem(equipmentModel.armorList[2]);
        else
            slots[2].ClearSlot();
        if (equipmentModel.weapon != null)
            slots[3].AddItem(equipmentModel.weapon);
        else
            slots[3].ClearSlot();
    }


    public void UseItem(EquipmentSlot equipmentSlot)
    {
        Item item = equipmentSlot.GetItem();
        if (item != null)
        {
            if (inventoryModel.inventoryItems.Count != inventoryModel.inventorySize)
            {
                equipmentModel.RemoveItem(item);
                inventoryModel.AddInventoryItem(item);
            }
            else if(notification.IsFree())
            {
                notification.SetText("There is no space in your inventory.");
                notification.ActiveOk();                     
            }
        }
    }
}
