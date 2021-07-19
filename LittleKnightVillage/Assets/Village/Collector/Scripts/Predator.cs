using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public float speed;
    public Collider dangerArea;
    public GameObject target;

    private void Update()
    {
        Hunt();
    }

    public void Hunt()
    {
        if (speed > 0 && target != null)
        {
            transform.LookAt(target.transform.position);
            Vector3 moveVector = speed * transform.forward * Time.fixedDeltaTime;
            if (moveVector.magnitude <= Vector3.Distance(transform.position, target.transform.position)
                   && dangerArea.bounds.Contains(target.transform.position))
            {
                transform.position += moveVector;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Villager"))
            collision.gameObject.GetComponent<VillagerAgent>().Die();
    }
}
