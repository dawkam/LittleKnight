using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    public double maxHealth;
    public double CurrentHealth { get; private set; }

    [Header("Armor")]   //default values 0-100%
    public double basePhysicalArmor;
    public double baseFireArmor;
    public double baseWaterArmor;
    public double baseEarthArmor;
    public double baseAirArmor;

    private double currentPhysicalArmor;
    private double currentFireArmor;
    private double currentWaterArmor;
    private double currentEarthArmor;
    private double currentAirArmor;

    public double CurrentPhysicalArmor
    {
        get => currentPhysicalArmor;
        set
        {
            if (value > 100)
                currentPhysicalArmor = 100;
            else if (value < 0)
                currentPhysicalArmor = 0;
            else
                currentPhysicalArmor = value;
        }
    }
    public double CurrentFireArmor
    {
        get => currentFireArmor;
        set
        {
            if (value > 100)
                currentFireArmor = 100;
            else if (value < 0)
                currentFireArmor = 0;
            else
                currentFireArmor = value;
        }
    }
    public double CurrentWaterArmor
    {
        get => currentWaterArmor;
        set
        {
            if (value > 100)
                currentWaterArmor = 100;
            else if (value < 0)
                currentWaterArmor = 0;
            else
                currentWaterArmor = value;
        }
    }
    public double CurrentEarthArmor
    {
        get => currentEarthArmor;
        set
        {
            if (value > 100)
                currentEarthArmor = 100;
            else if (value < 0)
                currentEarthArmor = 0;
            else
                currentEarthArmor = value;
        }
    }
    public double CurrentAirArmor
    {
        get => currentAirArmor;
        set
        {
            if (value > 100)
                currentAirArmor = 100;
            else if (value < 0)
                currentAirArmor = 0;
            else
                currentAirArmor = value;
        }
    }

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentPhysicalArmor = basePhysicalArmor;
        CurrentFireArmor = baseFireArmor;
        CurrentWaterArmor = baseWaterArmor;
        CurrentEarthArmor = baseEarthArmor;
        CurrentAirArmor = baseAirArmor;

    }

    public void TakeDamage(double damage, DamageType damageType)
    {

        damage = GetTrueDamage(damage, damageType);

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
        if (CurrentHealth != maxHealth)
        {
            CurrentHealth += healValue;
            Debug.Log(transform.name + healValue + " healed.");
            if (CurrentHealth > maxHealth)
                CurrentHealth = maxHealth;

        }
    }

    public virtual void Die()
    {
        //This method is meant to be overwrriten
        Debug.Log(transform.name + " died");
    }

    protected double GetTrueDamage(double damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                damage -= (damage * CurrentPhysicalArmor) / 100;
                break;
            case DamageType.Air:
                damage -= (damage * CurrentAirArmor) / 100;
                break;
            case DamageType.Water:
                damage -= (damage * CurrentWaterArmor) / 100;
                break;
            case DamageType.Fire:
                damage -= (damage * CurrentFireArmor) / 100;
                break;
            case DamageType.Earth:
                damage -= (damage * CurrentEarthArmor) / 100;
                break;
        }
        return damage;
    }
}

public enum DamageType { Physical, Air, Water, Fire, Earth }