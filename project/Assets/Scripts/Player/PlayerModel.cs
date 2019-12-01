using UnityEngine;


public class PlayerModel : CharacterStats
{
    [Header("Movement")]
    public float sprintSpeed;
    public float walkSpeed;
    public float rotationSpeed;
    public float jumpForce;

    public string[] GetStats()
    {
        string[] stats = new string[4];
        stats[0] ="Phys. armor: " +
             "\nAir armor: " +
             "\nWater armor: " +
             "\nFire armor: " +
             "\nEarth armor: " ;

        stats[1] = CurrentPhysicalArmor +
             "\n" + CurrentAirArmor +
             "\n" + CurrentWaterArmor +
             "\n" + CurrentFireArmor +
             "\n" + CurrentEarthArmor;
        stats[2] = "Phys. armor: " +
            "\nAir damage: " +
            "\nWater damage: " +
            "\nFire damage: " +
            "\nEarth damage: ";

        stats[3] = CurrentPhysicalDamage +
             "\n" + CurrentAirDamage +
             "\n" + CurrentWaterDamage +
             "\n" + CurrentFireDamage +
             "\n" + CurrentEarthDamage;


        return stats;
    }


}
