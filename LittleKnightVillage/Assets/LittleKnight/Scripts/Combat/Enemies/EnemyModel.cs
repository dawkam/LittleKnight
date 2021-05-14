using UnityEngine;
using UnityEngine.UI;

public class EnemyModel : CharacterStats
{
    public float lookRadius;

    public double baseAttackCooldown;
    [Header("Projectile")]
    public Transform firePoint;
    public float projectileSpeed;
    public float projectileLifeSpawn;
    public Projectile projectile;

    public bool isBoss;
    public float exp;

    public GameObject gameOverSreen;
    public Text text;
    public double CurrentAttackCooldown { get; set; }


    public void Fire(Vector3 target)
    {
        if (CurrentAttackCooldown <= 0)
        {
            Projectile newProjectile = Instantiate(projectile, firePoint.position, Quaternion.LookRotation((target - firePoint.position).normalized)) as Projectile;
            newProjectile.Speed = projectileSpeed;
            newProjectile.LifeSpawn = projectileSpeed;
            newProjectile.damage.Mul(damage);
            CurrentAttackCooldown = baseAttackCooldown;
        }
    }

    public void DecrementAttackCoolDown()
    {
        if (CurrentAttackCooldown > 0)
        {
            CurrentAttackCooldown -= Time.deltaTime;
        }
    }
}
