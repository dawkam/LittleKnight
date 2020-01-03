using System.Collections.Generic;
using UnityEngine;


public class PlayerModel : CharacterStats
{
    [Header("Movement")]
    public float sprintSpeed;
    public float walkSpeed;
    public float rotationSpeed;
    public float jumpForce;

    [Header("Level")]
    public int level = 1;
    public float exp = 0;
    public float expToNextLvl = 10;
    public float multiplier = 1.2f;
    public float healthPerLvl;

    [Header("Effects")]
    public GameObject LvlUpEffect;
    public string[] GetStats()
    {
        string[] stats = new string[4];
        stats[0] ="Phys. armor: " +
             "\nAir armor: " +
             "\nWater armor: " +
             "\nFire armor: " +
             "\nEarth armor: " ;

        foreach (float element in armor.elements)
        {
        stats[1] += element.ToString() + "\n" ;
        }
        stats[2] = "Phys. armor: " +
            "\nAir damage: " +
            "\nWater damage: " +
            "\nFire damage: " +
            "\nEarth damage: ";
        foreach (float element in damage.elements)
        {
            stats[3] += element.ToString() + "\n";
        }

        return stats;
    }

    public bool AddExp(float exp)
    {
        this.exp += exp;
        if (this.exp >= expToNextLvl)
        {
            LvlUp();
            return true;
        }
        return false;
    }

    private void LvlUp()
    {
        level++;
        this.exp -= expToNextLvl;
        expToNextLvl *= multiplier;
        baseHealth += healthPerLvl;
        CurrentHealth = baseHealth;

    }
}
