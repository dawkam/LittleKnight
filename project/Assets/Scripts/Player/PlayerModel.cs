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
        string[] stats = new string[2];
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

        return stats;
    }


}
