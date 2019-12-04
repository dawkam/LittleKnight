using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/")]
public class Armor : Item
{
    public ArmorType armorType;

    [Header("Armor")]   //values 0-100%
    public float physicalArmor; 
    public float fireArmor;
    public float waterArmor;
    public float earthArmor;
    public float airArmor;

    public Elements armor;

    private void OnEnable()
    {
        armor = new Elements(physicalArmor, airArmor, waterArmor,fireArmor, earthArmor);
        
    }

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
