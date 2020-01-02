using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Notification;

public class EquipmentController : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject equipmentUI;

    private PlayerController _playerController;
    private Notification notification;

    private EquipmentModel equipmentModel;
    private EquipmentView equipmentView;

    private InventoryController inventoryController;// do zmiany

    EquipmentSlot[] slots; // inventory slots pełnią funkcije widoku, 0 - hełm, 1 - tors, 2 - buty, 3 - broń

    private void Start()
    {
        equipmentModel = EquipmentModel.instance;
        equipmentModel.onEquipmentItemChangedCallback += UpdateUI;
        equipmentModel.onEquipmentItemChangedCallback += GetStats;

        equipmentView = GetComponent<EquipmentView>();

        inventoryController = InventoryController.instance;
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();

        GameObject player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        _playerController = player.GetComponent(typeof(PlayerController)) as PlayerController;

        SetDescription();

        notification = Notification.instance;
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
        {
            slots[3].AddItem(equipmentModel.weapon);
        }
        else
            slots[3].ClearSlot();

    }

    public void UseItem(EquipmentSlot equipmentSlot)
    {
        Item item = equipmentSlot.GetItem();
        if (item != null)
        {
            if (inventoryController.GetItemsCount() != inventoryController.GetInventorySize())
            {
                item.UnEquip();
                equipmentModel.RemoveItem(item);
                inventoryController.AddInventoryItem(item);
            }
            else if (notification.IsFree())
            {
                notification.ActiveOk("There is no space in your inventory.");
            }
        }
    }
    public void GetStats()
    {
        List<Armor> armors = equipmentModel.armorList;

        Elements fullArmor = new Elements(0, 0, 0, 0, 0);
        foreach (Armor armor in armors)
        {
            if (armor != null)
                fullArmor.Add(armor.armor);
        }
        _playerController.ChangeCurrentArmor(fullArmor);

        if (equipmentModel.weapon != null)
            _playerController.ChangeCurrentDamage(new Elements(equipmentModel.weapon.damage));
        else
            _playerController.ChangeCurrentDamage(new Elements(0, 0, 0, 0, 0));
        SetDescription();
    }


    private void SetDescription()
    {
        string[] stats = _playerController.GetStats();
        equipmentView.SetDescription(stats);

    }

    public Armor AddArmor(Armor armor)
    {
        return equipmentModel.AddArmor(armor);
    }

    public Weapon AddWeapon(Weapon weapon)
    {
        return equipmentModel.AddWeapon(weapon);
    }
}
