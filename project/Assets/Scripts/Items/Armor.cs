using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/")]
public class Armor : Item
{
    public ArmorType armorType;

    [Header("Armor")]
    #region DMG 
    //dmg dealt by weapon
    public double physicalArmor;
    public double fireArmor;
    public double waterArmor;
    public double earthArmor;
    public double airArmor;
    #endregion

    public override string GetDescription()
    {
        return "Physical Armor: " + physicalArmor  +
             "\nFire Armor: " + fireArmor +
             "\nWater Armor: " + waterArmor +
             "\nAir Armor: " + waterArmor +
             "\nEarth Armor: " + earthArmor;
    }

}

public enum ArmorType { Head, Chest, Feet}
