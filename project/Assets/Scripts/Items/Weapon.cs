using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    [Header("Damage")]
    #region DMG 
    //dmg dealt by weapon
    public double physicalDamage;
    public double fireDamage;
    public double waterDamage;
    public double earthDamage;
    public double airDamage;
    #endregion

    #region Pen
    [Header("Penetration")]
    //pen used by character not only weapon
    public double physicalPenetration;
    public double firePenetration;
    public double waterPenetration;
    public double earthPenetration;
    public double airPenetration;
    #endregion

    [Header("Others")]
    public double attackSpeed;

    public double lifeSteal; // 0-100%

    public double crtiChance; // 0-100%

    public override string GetDescription()
    {
        return "Physical Damage: " + physicalDamage  +
             "\nPhysical Penetration: "+ physicalPenetration +
             "\nAttack Speed: " + attackSpeed +
             "\nCrit. Chance: " + crtiChance  + 
             "\nLife Steal: " + attackSpeed  +
           "\n\nFire Penetration: "+ firePenetration +
             "\nWater Penetration: "+ waterPenetration +
             "\nAir Penetration: " + waterPenetration +
             "\nEarth Penetration: " + earthPenetration +
           "\n\nFire Damage: " + firePenetration +
             "\nWater Damage: " + waterPenetration +
             "\nAir Damage: " + waterPenetration +
             "\nEarth Damage: " + earthPenetration;
    }
}
