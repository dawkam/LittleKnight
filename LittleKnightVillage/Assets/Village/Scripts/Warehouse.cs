using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] public int foodCount { get; private set; }
    [SerializeField] public int woodCount { get; private set; }

    public void AddFood()
    {
        foodCount++;
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

    public void AddWood()
    {
        woodCount++;
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
