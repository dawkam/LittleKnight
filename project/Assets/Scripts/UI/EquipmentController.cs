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
                item.UnEquip();
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
    public void GetStats()
    {
        List<Armor> armors = equipmentModel.armorList;
        double armor;
        Weapon weapon = equipmentModel.weapon;
        double damage;
        //do przetestowania
        foreach (DamageType damageType in (DamageType[])Enum.GetValues(typeof(DamageType)))
        {
            armor = 0;
            damage = 0;
            switch (damageType)
            {
                case DamageType.Physical:
                    armor = armors.Sum(x => x!=null ? x.physicalArmor : 0);
                    damage = weapon != null ? weapon.physicalDamage : 0;
                    break;
                case DamageType.Air:
                    armor = armors.Sum(x => x != null ? x.airArmor : 0);
                    damage = weapon != null ? weapon.airDamage : 0;
                    break;
                case DamageType.Water:
                    armor = armors.Sum(x => x != null ? x.waterArmor : 0);
                    damage = weapon != null ? weapon.waterDamage : 0;
                    break;
                case DamageType.Fire:
                    armor = armors.Sum(x => x != null ? x.fireArmor : 0);
                    damage = weapon != null ? weapon.fireDamage : 0;
                    break;
                case DamageType.Earth:
                    armor = armors.Sum(x => x != null ? x.earthArmor : 0);
                    damage = weapon != null ? weapon.earthDamage : 0;
                    break;
            }
            _playerController.ChangeCurrentArmor(damageType, armor);
            _playerController.ChangeCurrentDamage(damageType, damage);
            SetDescription();
        }
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
