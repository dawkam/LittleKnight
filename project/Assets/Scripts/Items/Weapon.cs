using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    [Header("Damage")]
    #region DMG 
    //dmg dealt by weapon
    public float physicalDamage;
    public float fireDamage;
    public float waterDamage;
    public float earthDamage;
    public float airDamage;

    public Elements damage;
    #endregion
    private GameObject _weaponPlace;
    private GameObject _weapon;

    private void OnEnable()
    {
        damage = new Elements(physicalDamage, airDamage, waterDamage, fireDamage, earthDamage);
    }
    public override void Use()
    {
        if (_weaponPlace == null)
            _weaponPlace = GameObject.FindGameObjectWithTag("WeaponPlace");
        _weapon = Instantiate(prefab, _weaponPlace.transform);
    }

    public override string GetDescription()
    {
        return "Physical Damage: " + physicalDamage +
             "\nFire Damage: " + fireDamage +
             "\nWater Damage: " + waterDamage +
             "\nAir Damage: " + waterDamage +
             "\nEarth Damage: " + earthDamage;
    }

    public override void UnEquip()
    {
        Destroy(_weapon);
    }


    // scriptable object nie obsługuje on trgger
    //private void OnTriggerEnter(Collider other)
    //{
    //    CharacterStats parent = other.GetComponentInParent<CharacterStats>();
    //    if (parent != null && other.tag == "Player")
    //    {
    //        parent.TakeDamage(damage);

    //    }
    //}

    
}
