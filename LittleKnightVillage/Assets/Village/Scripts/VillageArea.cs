using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageArea : MonoBehaviour
{
    public TextMeshPro cumulativeRewardText;
    public VillagerAgent learningVillager;
    public GameObject fruitPrefab;
    public GameObject treePrefab;
    public GameObject woodPrefab;
    public Predator predator;
    public GameObject village;

    private ParametersGiver parametersGiver;
    private List<GameObject> fruitsList;
    private List<GameObject> treesList;
    private List<GameObject> woodsList;

    public int FruitsCount
    {
        get { return fruitsList.Count; }
    }
    public int TreesCount
    {
        get { return treesList.Count; }
    }

    private void Start()
    {
        parametersGiver = GetComponentInParent<ParametersGiver>();
        ResetArea();
    }

    private void Update()
    {
        cumulativeRewardText.text = learningVillager.GetCumulativeReward().ToString("0.00");
    }

    public void ResetArea()
    {
        ClearList(ref fruitsList);
        ClearList(ref treesList);
        ClearList(ref woodsList);
        PlaceGameObject(gameObject: village, minAngle: 90f, maxAngle: 180f, minRadius: 7f, maxRadius: 9f);

        PlaceGameObjectInSafeZone(learningVillager.gameObject);
        //PlaceGameObject(gameObject: predator.gameObject, minAngle: 270f, maxAngle: 360f, minRadius: 0f, maxRadius: 6f);
        SpawnManyObjects(fruitPrefab, fruitsList, Random.Range(parametersGiver.FriutsMinCount, parametersGiver.FriutsMaxCount));
        SpawnManyObjects(treePrefab, treesList, Random.Range(parametersGiver.WoodsMinCount, parametersGiver.WoodsMaxCount));
        PlaceGameObject(predator.gameObject, 270f, 360f, 5f, 7f);
        predator.target = null;
    }

    private void ClearList(ref List<GameObject> list)
    {
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    Destroy(list[i]);
                }
            }
        }

        list = new List<GameObject>();
    }

    public void PlaceGameObjectInSafeZone(GameObject prefab)
    {
        PlaceGameObject(gameObject: prefab, minAngle: 90f, maxAngle: 180f, minRadius: 7f, maxRadius: 10f);
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
    private void SpawnManyObjects(GameObject gameObject, List<GameObject> list, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gm = Instantiate(gameObject, transform);
            gm.transform.position = PlacementHelper.ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * 0.1f;
            gm.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            list.Add(gm);
        }
    }
    public void SpawnWoods(int count, Vector3 position)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gm = Instantiate(woodPrefab, transform);
            gm.transform.position = PlacementHelper.ChooseRandomPosition(position, 0f, 360f, 0f, 0.5f) + Vector3.up * 0.1f;
            woodsList.Add(gm);
        }
    }

    internal void RemoveSpecificCollectable(GameObject collectable)
    {
        fruitsList.Remove(collectable);
        Destroy(collectable);
    }
    internal void RemoveSpecificTree(GameObject tree)
    {
        SpawnWoods(parametersGiver.WoodSpawnCount, tree.transform.position);
        treesList.Remove(tree);
        Destroy(tree);
    }

    internal void RemoveSpecificWood(GameObject wood)
    {
        woodsList.Remove(wood);
        Destroy(wood);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager") && predator.target == null)
            predator.target = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager") && predator.target != null && predator.target.Equals(other.gameObject))
            predator.target = null;
    }
}
