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
    public Village village;

    private ParametersGiver parametersGiver;
    public ResearchData researchData = null;
    private List<GameObject> fruitsList;
    private List<GameObject> treesList;
    private List<GameObject> woodsList;
    private int collectDataIterator;

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
        parametersGiver = FindObjectOfType<ParametersGiver>();
        ResetArea();
        if (learningVillager.enabled == false)
            Debug.LogError("Inactive agent.");
    }

    private void Update()
    {
        cumulativeRewardText.text = learningVillager.GetCumulativeReward().ToString("0.00");
    }

    public void ResetArea()
    {
        StopCoroutine(CollectData());
        if (researchData != null)
            SaveData();


        ClearList(ref fruitsList);
        ClearList(ref treesList);
        ClearList(ref woodsList);

        researchData = new ResearchData();
        PlaceGameObject(gameObject: village.gameObject, minAngle: 90f, maxAngle: 180f, minRadius: 7f, maxRadius: 9f);

        PlaceGameObjectInSafeZone(learningVillager.gameObject);
        //PlaceGameObject(gameObject: predator.gameObject, minAngle: 270f, maxAngle: 360f, minRadius: 0f, maxRadius: 6f);
        PlaceGameObject(predator.gameObject, 270f, 360f, 5f, 7f);
        predator.target = null;
        StopCoroutine(ResourcesSpawn());
        StopCoroutine(CollectData());

        StartCoroutine(ResourcesSpawn());
        StartCoroutine(CollectData());

    }

    private void SaveData()
    {
        researchData.collectorsCount = village.GetCollectorsCount();
        researchData.lumberjacksCount = village.GetLumberjacksCount();
        researchData.artisansCount = village.GetArtisansCount();
        researchData.babysCount = village.GetBabysCount();

        researchData.warehouseFoodCountAverage /= collectDataIterator;
        researchData.warehouseWoodCountAverage /= collectDataIterator;
        researchData.sceneFoodCountAverage /= collectDataIterator;
        researchData.sceneWoodCountAverage /= collectDataIterator;
        researchData.sceneTreeCountAverage /= collectDataIterator;

        FindObjectOfType<CSVManager>().AddData(researchData);

    }

    IEnumerator CollectData()
    {
        collectDataIterator = 0;
        while (true)
        {
            researchData.warehouseFoodCountMax = Mathf.Max(researchData.warehouseFoodCountMax, village.warehouse.FoodCount);
            researchData.warehouseFoodCountMin = Mathf.Min(researchData.warehouseFoodCountMin, village.warehouse.FoodCount);
            researchData.warehouseFoodCountAverage += village.warehouse.FoodCount;
            collectDataIterator++;

            researchData.sceneFoodCountAverage += fruitsList.Count;

            researchData.warehouseWoodCountMax = Mathf.Max(researchData.warehouseWoodCountMax, village.warehouse.WoodCount);
            researchData.warehouseWoodCountMin = Mathf.Min(researchData.warehouseWoodCountMin, village.warehouse.WoodCount);
            researchData.warehouseWoodCountAverage += village.warehouse.WoodCount;

            researchData.sceneWoodCountAverage += woodsList.Count;
            researchData.sceneTreeCountAverage += treesList.Count;

            yield return new WaitForSeconds(parametersGiver.CollectDataTime);
        }
    }

    IEnumerator ResourcesSpawn()
    {
        while (true)
        {
            if (fruitsList.Count < parametersGiver.FruitOnSceneMax)
                SpawnManyObjects(fruitPrefab, fruitsList, Random.Range(parametersGiver.FriutsMinCount, parametersGiver.FriutsMaxCount));
            if (treesList.Count < parametersGiver.TreesOnSceneMax)
                SpawnManyObjects(treePrefab, treesList, Random.Range(parametersGiver.WoodsMinCount, parametersGiver.WoodsMaxCount));

            yield return new WaitForSeconds(parametersGiver.ResourcesSpawnTime);
        }
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
