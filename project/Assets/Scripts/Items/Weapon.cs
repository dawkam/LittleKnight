using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    [Header("Damage")]
    #region DMG 
    //dmg dealt by weapon
    public double physicalDamage;
    public double fireDamage;
    public double waterDamage;
    public double earthDamage;
    public double airDamage;
    #endregion
    private GameObject _weaponPlace;
    private GameObject _weapon;

    public override void Use()
    {
        if(_weaponPlace==null)
            _weaponPlace = GameObject.FindGameObjectWithTag("WeaponPlace");
        _weapon=Instantiate(prefab, _weaponPlace.transform);
    }

    public override string GetDescription()
    {
        return "Physical Damage: " + physicalDamage  +
             "\nFire Damage: " + fireDamage +
             "\nWater Damage: " + waterDamage +
             "\nAir Damage: " + waterDamage +
             "\nEarth Damage: " + earthDamage;
    }

    public override void UnEquip()
    {
        Destroy(_weapon);
    }
}
