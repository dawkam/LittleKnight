﻿using System;
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
    public Text defStats;
    public Text defValue;
    public Text dmgStats;
    public Text dmgValue;

    EquipmentModel equipmentModel;
    InventoryModel inventoryModel;

    EquipmentSlot[] slots; // inventory slots pełnią funkcije widoku, 0 - hełm, 1 - tors, 2 - buty, 3 - broń

    private void Start()
    {
        equipmentModel = EquipmentModel.instance;
        equipmentModel.onEquipmentItemChangedCallback += UpdateUI;
        equipmentModel.onEquipmentItemChangedCallback += GetStats;

        inventoryModel = InventoryModel.instance;
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();

        GameObject player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        _playerController = player.GetComponent(typeof(PlayerController)) as PlayerController;

        SetDescription();

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
            if (inventoryModel.inventoryItems.Count != inventoryModel.inventorySize)
            {
                item.UnEquip();
                equipmentModel.RemoveItem(item);
                inventoryModel.AddInventoryItem(item);
            }
            else if (notification.IsFree())
            {
                notification.SetText("There is no space in your inventory.");
                notification.ActiveOk();
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
            _playerController.ChangeCurrentDamage(equipmentModel.weapon.damage);
        else
            _playerController.ChangeCurrentDamage(_playerController.GetBaseDamage());
        SetDescription();
    }


    private void SetDescription()
    {
        string[] stats = _playerController.GetStats();
        defStats.text = stats[0];
        defValue.text = stats[1];
        dmgStats.text = stats[2];
        dmgValue.text = stats[3];

    }
}
