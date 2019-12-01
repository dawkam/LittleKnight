using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/")]
public class Armor : Item
{
    public ArmorType armorType;

    [Header("Armor")]   //values 0-100%
    public double physicalArmor; 
    public double fireArmor;
    public double waterArmor;
    public double earthArmor;
    public double airArmor;


    public override string GetDescription()
    {
        return "Physical Armor: " + physicalArmor  +
             "\nFire Armor: " + fireArmor +
             "\nWater Armor: " + waterArmor +
             "\nAir Armor: " + airArmor +
             "\nEarth Armor: " + earthArmor;
    }

}

public enum ArmorType { Head, Chest, Feet}
