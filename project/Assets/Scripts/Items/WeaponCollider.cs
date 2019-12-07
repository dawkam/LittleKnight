using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private Elements damage;

    public void SetDamage(Elements damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    { 
        CharacterStats parent = other.GetComponentInParent<CharacterStats>();
        if (parent != null && other.tag != "Player")
        {
            parent.TakeDamage(damage);

        }
    }
}
