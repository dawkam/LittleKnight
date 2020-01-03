using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentModel : MonoBehaviour
{
    #region Singleton
    public static EquipmentModel instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Lootsystem !");
        }

        instance = this;
    }
    #endregion

    public delegate void OnEquipmentItemChanged();
    public OnEquipmentItemChanged onEquipmentItemChangedCallback;

    public Weapon weapon;
    public List<Armor> armorList = new List<Armor>(3); // 3 sloty hełm, zbroja, buty

    public Weapon AddWeapon(Weapon weapon)
    {
        Weapon oldWeapon = null;
        if (weapon == null)
            this.weapon = weapon;
        else
        {
            oldWeapon = this.weapon;
            this.weapon = weapon;
        }
        if (onEquipmentItemChangedCallback != null)
            onEquipmentItemChangedCallback.Invoke();
        return oldWeapon;
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            if (item.GetType() == typeof(Weapon))
                RemoveWeapon();
            else
                RemoveArmor(((Armor)item).armorType);
        }
        if (onEquipmentItemChangedCallback != null)
            onEquipmentItemChangedCallback.Invoke();
    }

    public Weapon RemoveWeapon()
    {
        Weapon oldWeapon =weapon;
        this.weapon = null;
        if (onEquipmentItemChangedCallback != null)
            onEquipmentItemChangedCallback.Invoke();
        return oldWeapon;
    }

    public Armor AddArmor(Armor armor)
    {
        Armor oldArmor=null;
        switch (armor.armorType)
        {
            case ArmorType.Head:
                if (armorList[0] == null)
                    armorList[0] = armor;
                else
                {
                    oldArmor = armorList[0];
                    armorList[0] = armor;
                }
                break;
            case ArmorType.Chest:
                if (armorList[1] == null)
                    armorList[1] = armor;
                else
                {
                    oldArmor = armorList[1];
                    armorList[1] = armor;
                }
                break;
            case ArmorType.Feet:
                if (armorList[2] == null)
                    armorList[2] = armor;
                else
                {
                    oldArmor = armorList[2];
                    armorList[2] = armor;
                }
                break;

        }
        if (onEquipmentItemChangedCallback != null)
            onEquipmentItemChangedCallback.Invoke();

        return oldArmor;
    }

    public Armor RemoveArmor(ArmorType armorType)
    {
        Armor oldArmor = null;
        switch (armorType)
        {
            case ArmorType.Head:
                if (armorList[0] != null)
                {
                    oldArmor = armorList[0];
                    armorList[0] = null;
                }
                break;
            case ArmorType.Chest:
                if (armorList[1] != null)
                {
                    oldArmor = armorList[1];
                    armorList[1] = null;
                }
                break;
            case ArmorType.Feet:
                if (armorList[2] != null)
                {
                    oldArmor = armorList[2];
                    armorList[2] = null;
                }
                break;

        }
        if (onEquipmentItemChangedCallback != null)
            onEquipmentItemChangedCallback.Invoke();
        return oldArmor;
    }

    public int CountOfItem(string name)
    {
        int result = 0;
        if (weapon != null)
        {
            result = (weapon.name == name ? 1 : 0);
        }
        result += armorList.Where(x =>x !=null && x.name == name).Count();
        return   result ;
    }

}

