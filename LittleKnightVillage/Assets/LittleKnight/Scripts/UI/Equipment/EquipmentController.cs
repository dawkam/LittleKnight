using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Notification;

public class EquipmentController : MonoBehaviour
{
   
    private PlayerController _playerController;
    private Notification _notification;

    private EquipmentModel _equipmentModel;
    private EquipmentView _equipmentView;

    private InventoryController _inventoryController;// do zmiany

    private EquipmentSlot[] _slots; // inventory slots pełnią funkcije widoku, 0 - hełm, 1 - tors, 2 - buty, 3 - broń

    private void Start()
    {
        _equipmentModel = EquipmentModel.instance;
        _equipmentModel.onEquipmentItemChangedCallback += UpdateUI;
        _equipmentModel.onEquipmentItemChangedCallback += GetStats;

        _equipmentView = GetComponent<EquipmentView>();

        _inventoryController = InventoryController.instance;
        _slots = _equipmentModel.itemsParent.GetComponentsInChildren<EquipmentSlot>();

        GameObject player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        _playerController = player.GetComponent(typeof(PlayerController)) as PlayerController;

        SetDescription();

        _notification = Notification.instance;
    }

    void UpdateUI()
    {
        if (_equipmentModel.armorList[0] != null)
        {
            _slots[0].AddItem(_equipmentModel.armorList[0]);

        }
        else
            _slots[0].ClearSlot();

        if (_equipmentModel.armorList[1] != null)
            _slots[1].AddItem(_equipmentModel.armorList[1]);
        else
            _slots[1].ClearSlot();

        if (_equipmentModel.armorList[2] != null)
            _slots[2].AddItem(_equipmentModel.armorList[2]);
        else
            _slots[2].ClearSlot();
        if (_equipmentModel.weapon != null)
        {
            _slots[3].AddItem(_equipmentModel.weapon);
        }
        else
            _slots[3].ClearSlot();

    }

    public void UseItem(EquipmentSlot equipmentSlot)
    {
        Item item = equipmentSlot.GetItem();
        if (item != null)
        {
            if (_inventoryController.GetItemsCount() != _inventoryController.GetInventorySize())
            {
                item.UnEquip();
                _equipmentModel.RemoveItem(item);
                _inventoryController.AddInventoryItem(item);
            }
            else if (_notification.IsFree())
            {
                _notification.ActiveOk("There is no space in your inventory.");
            }
        }
    }
    public void GetStats()
    {
        List<Armor> armors = _equipmentModel.armorList;

        Elements fullArmor = new Elements(0, 0, 0, 0, 0);
        foreach (Armor armor in armors)
        {
            if (armor != null)
                fullArmor.Add(armor.armor);
        }
        _playerController.ChangeCurrentArmor(fullArmor);

        if (_equipmentModel.weapon != null)
            _playerController.ChangeCurrentDamage(new Elements(_equipmentModel.weapon.damage));
        else
            _playerController.ChangeCurrentDamage(new Elements(0, 0, 0, 0, 0));
        SetDescription();
    }


    private void SetDescription()
    {
        string[] stats = _playerController.GetStats();
        _equipmentView.SetDescription(stats);

    }

    public Armor AddArmor(Armor armor)
    {
        return _equipmentModel.AddArmor(armor);
    }

    public Weapon AddWeapon(Weapon weapon)
    {
        return _equipmentModel.AddWeapon(weapon);
    }

    public int CountOfItem(string name)
    {
        return _equipmentModel.CountOfItem(name);
    }
}
