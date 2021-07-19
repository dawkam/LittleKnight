using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] public int foodCount { get; private set; }
    [SerializeField] public int woodCount { get; private set; }

    private void Start()
    {
        foodCount = Random.Range(0, 0);
        woodCount = Random.Range(0, 3);
    }

    public void AddFood(int count)
    {
        foodCount += count;
    }

    public bool TakeFood()
    {
        if (foodCount > 0)
        {
            foodCount--;
            return true;
        }
        else
            return false;
    }

    public void AddWood(int count)
    {
        woodCount += count;
    }

    public bool TakeWood()
    {
        if (woodCount > 0)
        {
            woodCount--;
            return true;
        }
        else
            return false;
    }

}
