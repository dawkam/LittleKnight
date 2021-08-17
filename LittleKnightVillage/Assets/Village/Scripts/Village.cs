using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour, IObservable
{
    private float comfort;
    List<IObserver> observers = new List<IObserver>();
    private ParametersGiver parametersGiver;

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
        parametersGiver = GetComponentInParent<ParametersGiver>();
        ResetData();
    }
    private void Update()
    {
        if (Comfort > parametersGiver.ComfortMin)
            Comfort -= parametersGiver.ComfortTick;

    }

    public void ResetData()
    {
        StopCoroutine(ComfortConsumption());
        Comfort = parametersGiver.ComfortMin; //+ Random.Range(0, 10);
        observers.Clear();
        StartCoroutine(ComfortConsumption());
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
}
