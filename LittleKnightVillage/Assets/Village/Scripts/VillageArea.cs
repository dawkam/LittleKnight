using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageArea : MonoBehaviour
{
    public TextMeshPro cumulativeRewardText;
    public VillagerAgent villager;
    public GameObject fruitPrefab;
    public GameObject predator;
    public GameObject village;

    private List<GameObject> collectableList;
    public int CollectableCount
    {
        get { return collectableList.Count; }
    }

    private void Start()
    {
        ResetArea();
    }

    private void Update()
    {
        cumulativeRewardText.text = villager.GetCumulativeReward().ToString("0.00");
    }

    public void ResetArea()
    {
        RemoveAllCollectable();
        PlaceGameObject(gameObject: village, minAngle: 90f, maxAngle: 180f, minRadius: 7f, maxRadius: 9f);

        PlaceGameObject(gameObject: villager.gameObject, minAngle: 90f, maxAngle: 180f, minRadius: 7f, maxRadius: 10f);
        PlaceGameObject(gameObject: predator, minAngle: 270f, maxAngle: 360f, minRadius: 0f, maxRadius: 6f);
        SpawnFruits();
    }

    private void RemoveAllCollectable()
    {
        if (collectableList != null)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                if (collectableList[i] != null)
                {
                    Destroy(collectableList[i]);
                }
            }
        }

        collectableList = new List<GameObject>();
    }

    private void PlaceGameObject(GameObject gameObject, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        gameObject.transform.position = PlacementHelper.ChooseRandomPosition(transform.position, minAngle, maxAngle, minRadius, maxRadius) + Vector3.up * .2f;
        gameObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

    }
    private void SpawnFruits()
    {
        int count = Random.Range(3, 5);
        for (int i = 0; i < count; i++)
        {
            GameObject fruit = Instantiate(fruitPrefab, transform);
            fruit.transform.position = PlacementHelper.ChooseRandomPosition(transform.position, 0f, 360f, 0f, 8f) + Vector3.up * 0.1f;
            fruit.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            collectableList.Add(fruit);
        }
    }

    internal void RemoveSpecificCollectable(GameObject collectable)
    {
        collectableList.Remove(collectable);
        Destroy(collectable);
    }

}
