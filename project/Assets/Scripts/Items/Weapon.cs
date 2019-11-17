using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    [Header("Damage")]
    #region DMG 
    //dmg dealt by weapon
    public double _physicalDamage;
    public double _fireDamage;
    public double _waterDamage;
    public double _earthDamage;
    public double _airDamage;
    #endregion

    #region Pen
    [Header("Penetration")]
    //pen used by character not only weapon
    public double _physicalPenetration;
    public double _firePenetartion;
    public double _waterPenetartion;
    public double _earthPenetartion;
    public double _airPenetartion;
    #endregion

    [Header("Others")]
    public double attackSpeed;

    public double _lifeSteal; // 0-100%

    public double _crtiChance; // 0-100%

}
