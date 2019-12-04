using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    public string characterName;

    [Header("Health")]
    public double baseHealth;
    public double CurrentHealth { get; private set; }

    [Header("Armor")]   //default values 0-100%
    public float basePhysicalArmor = 0;
    public float baseFireArmor;
    public float baseWaterArmor;
    public float baseEarthArmor;
    public float baseAirArmor;

    public Elements baseArmor;
    public Elements armor;

    public float basePhysicalDamage;
    public float baseFireDamage;
    public float baseWaterDamage;
    public float baseEarthDamage;
    public float baseAirDamage;

    public Elements baseDamage;
    public Elements damage;

    protected virtual void Awake()
    {
        CurrentHealth = baseHealth;
        baseArmor = new Elements(basePhysicalArmor, baseAirArmor, baseWaterArmor, baseFireArmor, baseEarthArmor);
        armor = new Elements(basePhysicalArmor, baseAirArmor, baseWaterArmor, baseFireArmor, baseEarthArmor);
        baseDamage = new Elements(basePhysicalDamage, baseAirDamage, baseWaterDamage, baseFireDamage, baseEarthDamage);
        damage = new Elements(basePhysicalDamage, baseAirDamage, baseWaterDamage, baseFireDamage, baseEarthDamage);
    }

    public void TakeDamage(Elements incomeDamage)
    {
        float damage = armor.GetTrueDamage(incomeDamage);

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    public void Heal(double healValue)
    {
        if (CurrentHealth != baseHealth)
        {
            CurrentHealth += healValue;
            Debug.Log(transform.name + healValue + " healed.");
            if (CurrentHealth > baseHealth)
                CurrentHealth = baseHealth;

        }
    }

    public virtual void Die()
    {
        //This method is meant to be overwrriten
        Debug.Log(transform.name + " died");
    }


}


