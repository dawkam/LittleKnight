using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour, IObservable
{
    private float comfort;
    List<IObserver> observers = new List<IObserver>();
    private ParametersGiver parametersGiver;

    public float Comfort { get => comfort; private set => comfort = value; }

    private void Awake()
    {
        parametersGiver = GetComponentInParent<ParametersGiver>();
        Reset();
    }

    private void Udpate()
    {
        if (Comfort > parametersGiver.ComfortMin)
            Comfort -= parametersGiver.ComfortTick;
    }


    public void Reset()
    {
        StopCoroutine(ComfortConsumption());
        Comfort = parametersGiver.ComfortMin;
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
