using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region DMG 
    //dmg dealt by weapon
    private double _physicalDamage { get; set; }    
    private double _fireDamage { get; set; }        
    private double _waterDamage { get; set; }       
    private double _earthDamage { get; set; }       
    private double _airDamage { get; set; }
    #endregion

    #region Pen
    //pen used by character not only weapon
    private double _physicalPenetration { get; set; }
    private double _firePenetartion { get; set; }
    private double _waterPenetartion { get; set; }
    private double _earthPenetartion { get; set; }
    private double _airPenetartion { get; set; }
    #endregion

    private double attackSpeed;

    private double _lifeSteal; // 0-100%

    private double _crtiChance; // 0-100%
    private bool _isEquipped { get; set; }
    private double _durability { get; set; }
    private double _price { get; set; }

    public Weapon(double physicalDamage, double fireDamage, double waterDamage, double earthDamage, double airDamage, double physicalPenetration, double firePenetartion, double waterPenetartion, double earthPenetartion, double airPenetartion, double attackSpeed, double lifeSteal, double crtiChance, bool isEquipped, double durability, double price)
    {
        _physicalDamage = physicalDamage;
        _fireDamage = fireDamage;
        _waterDamage = waterDamage;
        _earthDamage = earthDamage;
        _airDamage = airDamage;
        _physicalPenetration = physicalPenetration;
        _firePenetartion = firePenetartion;
        _waterPenetartion = waterPenetartion;
        _earthPenetartion = earthPenetartion;
        _airPenetartion = airPenetartion;
        this.attackSpeed = attackSpeed;
        _lifeSteal = lifeSteal;
        _crtiChance = crtiChance;
        _isEquipped = isEquipped;
        _durability = durability;
        _price = price;
    }
}
