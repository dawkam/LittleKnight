﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersGiver : MonoBehaviour
{
    #region Villager

    [Header("Hunger")]
    [SerializeField] private float hungerMax;
    [SerializeField] private float hungerTick;
    [SerializeField] private float foodValue;

    [Header("Thirst")]
    [SerializeField] private float thirstMax;
    [SerializeField] private float thirstTick;
    [SerializeField] private float waterValue;

    [Header("Stamina")]
    [SerializeField] private float comfrotMin;
    [SerializeField] private float comfortTick;
    [SerializeField] private float staminaTick;
    [SerializeField] private float staminaDashTick;
    [SerializeField] private float restTime;


    [Header("Movement")]
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private float dashPower;
    [SerializeField] private float turnSpeed;


    [Header("Collector")]
    [SerializeField] private int foodBagSize;

    #region Existence
    public float HungerMax
    {
        get
        {
            if (hungerMax == 0)
                Debug.LogError("hungerMax is 0!!");
            return hungerMax;
        }
        private set => hungerMax = value;
    }
    public float HungerTick
    {
        get
        {
            if (hungerTick == 0)
                Debug.LogError("hungerTick is 0!!");
            return hungerTick;
        }
        private set => hungerTick = value;
    }
    public float FoodValue
    {
        get
        {
            if (foodValue == 0)
                Debug.LogError("foodValue is 0!!");
            return foodValue;
        }
        private set => foodValue = value;
    }

    public float ThirstMax
    {
        get
        {
            if (thirstMax == 0)
                Debug.LogError("thirstMax is 0!");
            return thirstMax;
        }
        private set => thirstMax = value;
    }
    public float ThirstTick
    {
        get
        {
            if (thirstTick == 0)
                Debug.LogError("thirstTick is 0!");
            return thirstTick;
        }
        private set => thirstTick = value;
    }
    public float WaterValue
    {
        get
        {
            if (waterValue == 0)
                Debug.LogError("waterValue is 0!!");
            return waterValue;
        }
        private set => waterValue = value;
    }

    public float ComfortMin
    {
        get
        {
            if (comfrotMin == 0)
                Debug.LogError("comfrotMin is 0!!");
            return comfrotMin;
        }
        private set => comfrotMin = value;
    }
    public float ComfortTick
    {
        get
        {
            if (comfortTick == 0)
                Debug.LogError("comfortTick is 0!!");
            return comfortTick;
        }
        private set => comfortTick = value;
    }
    public float StaminaTick
    {
        get
        {
            if (staminaTick == 0)
                Debug.LogError("staminaTick is 0!!");
            return staminaTick;
        }
        private set => staminaTick = value;
    }
    #endregion

    #region Movement
    public float MoveSpeedMax
    {
        get
        {
            if (moveSpeedMax == 0)
                Debug.LogError("moveSpeed is 0!!");
            return moveSpeedMax;
        }
        private set => moveSpeedMax = value;
    }

    public float DashPower
    {
        get
        {
            if (dashPower == 0)
                Debug.LogError("dashPower is 0!!");
            return dashPower;
        }
        private set => dashPower = value;
    }
    public float TurnSpeed
    {
        get
        {
            if (turnSpeed == 0)
                Debug.LogError("turnSpeed is 0!!");
            return turnSpeed;
        }
        private set => turnSpeed = value;
    }
    public float RestTime
    {
        get
        {
            if (restTime == 0)
                Debug.LogError("restTime is 0!!");
            return restTime;
        }
        private set => restTime = value;
    }

    public float StaminaDashTick
    {
        get
        {
            if (staminaDashTick == 0)
                Debug.LogError("staminaDashTick is 0!!");
            return staminaDashTick;
        }
        private set => staminaDashTick = value;
    }


    #endregion
    #endregion
    public int FoodBagSize 
    {
        get
        {
            if (foodBagSize == 0)
                Debug.LogError("foodBagSize is 0!!");
            return foodBagSize;
        }
        private set => foodBagSize = value;

    }

    #region World


    #endregion

    public void ResetParametrs() 
    {
        FoodBagSize = Random.Range(3, 5);
        ComfortMin = Random.Range(5, 15);

    }
}
