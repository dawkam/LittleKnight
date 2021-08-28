using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] public int FoodCount { get; private set; }
    [SerializeField] public int WoodCount { get; private set; }

    private void Start()
    {
        FoodCount = Random.Range(3, 5);
        WoodCount = Random.Range(3, 5);
    }

    public void AddFood(int count)
    {
        FoodCount += count;
    }

    public bool TakeFood()
    {
        if (FoodCount > 0)
        {
            FoodCount--;
            return true;
        }
        else
            return false;
    }

    public void AddWood(int count)
    {
        WoodCount += count;
    }

    public bool TakeWood()
    {
        if (WoodCount > 0)
        {
            WoodCount--;
            return true;
        }
        else
            return false;
    }

}
