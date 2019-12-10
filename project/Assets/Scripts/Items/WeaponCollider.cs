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

    private void OnTriggerExit(Collider other)
    { 
        EnemyController parent = other.GetComponentInParent<EnemyController>();
        if (parent != null && other.tag != "Player")
        {
            parent.TakeDamage(damage);

        }
    }
}
