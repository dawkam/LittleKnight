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
    public double firePenetartion;
    public double waterPenetartion;
    public double earthPenetartion;
    public double airPenetartion;
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
           "\n\nFire Penetartion: "+ firePenetartion +
             "\nWater Penetartion: "+ waterPenetartion +
             "\nAir Penetartion: " + waterPenetartion +
             "\nEarth Penetration: " + earthPenetartion +
           "\n\nFire Damage: " + firePenetartion +
             "\nWater Damage: " + waterPenetartion +
             "\nAir Damage: " + waterPenetartion +
             "\nEarth Damage: " + earthPenetartion;
    }
}
