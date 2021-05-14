using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public float speed;
    public void Hunt(Vector3 targetPosition)
    {
        if (speed > 0)
        {
            transform.LookAt(targetPosition);
            Vector3 moveVector = speed * transform.forward * Time.fixedDeltaTime;
            if (moveVector.magnitude <= Vector3.Distance(transform.position, targetPosition))
            {
                transform.position += moveVector;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Collector"))
            collision.gameObject.GetComponent<CollectorAgent>().Die();
    }
}
