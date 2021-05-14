using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorArea : MonoBehaviour
{
    public GameObject baby;
    public GameObject predator;
    public GameObject collector;
    public GameObject fruitPrefab;

    public float radius;

    private List<GameObject> collectableList;
    public int CollectableCount
    {
        get { return collectableList.Count; }
    }

    private void Start()
    {
        ResetArea();
    }

    public void ResetArea()
    {
        collectableList = new List<GameObject>();
        PlaceGameObject(baby, 90f, 180f, 10f, 14f);
        baby.transform.LookAt(transform.position);
        PlaceGameObject(collector, 90f, 180f, 10f, 14f);
        PlaceGameObject(predator, 0f, 360f, 0f, 8f);
        SpawnFruits();
    }

    private void FixedUpdate()
    {
        Hunt();
    }

    private void PlaceGameObject(GameObject gameObject, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        gameObject.transform.position = PlacementHelper.ChooseRandomPosition(transform.position, minAngle, maxAngle, minRadius, maxRadius) + Vector3.up * .5f;
        gameObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

    }

    private void SpawnFruits()
    {
        int count = Random.Range(3, 10);
        for (int i = 0; i < count; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab, transform);
            fruit.transform.position = PlacementHelper.ChooseRandomPosition(transform.position, 0f, 360f, 0f, 8f) + Vector3.up * 0.1f;
            fruit.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            collectableList.Add(fruit);
        }
    }


    private void Hunt()
    {
        float x0 = transform.position.x;
        float z0 = transform.position.z;
        float x = collector.transform.position.x;
        float z = collector.transform.position.z;

        if (Mathf.Pow(x0 - x, 2) + Mathf.Pow(z0 - z, 2) < Mathf.Pow(radius, 2))
        {
            predator.GetComponent<Predator>().Hunt(collector.transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    internal void RemoveSpecificCollectable(GameObject collectable)
    {
        collectableList.Remove(collectable);
        Destroy(collectable);
    }
}
