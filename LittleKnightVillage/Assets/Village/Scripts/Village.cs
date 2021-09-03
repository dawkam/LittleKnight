using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Village : MonoBehaviour, IObservable
{
    private float comfort;
    List<IObserver> observers = new List<IObserver>();
    private ParametersGiver parametersGiver;

    public GameObject well;
    public Warehouse warehouse;

    public GameObject CollectorPrefab;
    public GameObject LumberjackPrefab;
    public GameObject ArtisanPrefab;
    public GameObject BabyPrefab;

    public List<GameObject> collectors;

    public List<GameObject> lumberjacks;

    public List<GameObject> artisans;

    public List<GameObject> babys;


    public float Comfort
    {
        get => comfort;

        private set
        {
            comfort = value;
            NotifyObservers();
        }
    }

    private void Awake()
    {
        parametersGiver = FindObjectOfType<ParametersGiver>();
        ResetData();
    }
    public void ResetData()
    {
        StopCoroutine(ComfortConsumption());
        Comfort = parametersGiver.ComfortMin; /*+ Random.Range(0, 10)*/;
        observers.Clear();
        StartCoroutine(ComfortConsumption());

        ClearList(ref collectors);

        ClearList(ref lumberjacks);

        ClearList(ref artisans);

        ClearList(ref babys);

        warehouse.ResetData();
    }

    IEnumerator ComfortConsumption()
    {
        while (true)
        {
            if (Comfort > parametersGiver.ComfortMin)
                Comfort -= parametersGiver.ComfortTick;
            yield return new WaitForSeconds(1f);
        }
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
        observer.UpdateObserver();
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver obs in observers)
            obs.UpdateObserver();
    }

    public void AddComfort()
    {
        Comfort += parametersGiver.ComfortTick * 50;
    }

    public int GetVillagersCount()
    {
        return 1 + GetCollectorsCount() + GetLumberjacksCount() + GetArtisansCount() + GetBabysCount();
    }
    public int GetCollectorsCount()
    {
        return collectors.Count;
    }
    public int GetLumberjacksCount()
    {
        return lumberjacks.Count;
    }
    public int GetArtisansCount()
    {
        return artisans.Count;
    }

    public int GetBabysCount()
    {
        return babys.Count;
    }
    public void SpawnVillager(VillagerRole villagerRole, Vector3 position)
    {
        switch (villagerRole)
        {
            case VillagerRole.Collector:
                SpawnSpecificVillager(CollectorPrefab, collectors, position);
                break;
            case VillagerRole.Lumberjack:
                SpawnSpecificVillager(LumberjackPrefab, lumberjacks, position);
                break;
            case VillagerRole.Artisan:
                SpawnSpecificVillager(ArtisanPrefab, artisans, position);
                break;
        }
    }

    private GameObject SpawnSpecificVillager(GameObject prefab, List<GameObject> list, Vector3 position)
    {
        GameObject gm = Instantiate(prefab, position, gameObject.transform.rotation, gameObject.transform.parent);
        list.Add(gm);
        return gm;
    }

    public void SpawnBaby(VillagerRole villagerRole)
    {
        GameObject gm = SpawnSpecificVillager(BabyPrefab, babys, transform.position);

        VillageArea villageArea = transform.parent.GetComponent<VillageArea>();
        villageArea.PlaceGameObjectInSafeZone(gm);

        gm.GetComponent<BabyAgent>().StartGrowing(villagerRole);
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

    private void OnTriggerEnter(Collider other)
    {
        ArtisanAgent artisanAgent = other.gameObject.GetComponent<ArtisanAgent>();
        if (artisanAgent != null)
            artisanAgent.isInsideVillage = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ArtisanAgent artisanAgent = other.gameObject.GetComponent<ArtisanAgent>();
        if (artisanAgent != null)
            artisanAgent.isInsideVillage = false;
    }
}
