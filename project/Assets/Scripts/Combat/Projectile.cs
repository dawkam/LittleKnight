using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float physicalDamage;
    public float fireDamage;
    public float waterDamage;
    public float earthDamage;
    public float airDamage;

    public Elements damage;
    public float LifeSpawn { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        damage = new Elements(physicalDamage, airDamage, waterDamage, fireDamage, earthDamage);
    }
    // Update is called once per frame
    void Update()
    {
        LifeSpawn -= Time.deltaTime;
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (LifeSpawn <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats parent = other.GetComponentInParent<CharacterStats>();
        if (parent != null && other.tag == "Player")
            parent.TakeDamage(damage);

        if (other.tag != "Particles")
            Destroy(gameObject);
    }



}
