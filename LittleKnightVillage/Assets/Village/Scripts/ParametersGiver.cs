using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersGiver : MonoBehaviour
{
    #region Villager
    [SerializeField] private float hungerMax;
    [SerializeField] private float hungerTick;

    [SerializeField] private float thirstMax;
    [SerializeField] private float thirstTick;

    [SerializeField] private float staminaMax;
    [SerializeField] private float staminaTick;

    [SerializeField] private float moveSpeedMax;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float restTime;

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
    public float StaminaMax
    {
        get
        {
            if (staminaMax == 0)
                Debug.LogError("staminaMax is 0!!");
            return staminaMax;
        }
        private set => staminaMax = value;
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

    #endregion
    #endregion

    #region World
    [SerializeField] private float foodValue;
    [SerializeField] private float waterValue;
    public float FoodValue { get => foodValue; private set => foodValue = value; }
    public float WaterValue { get => waterValue; private set => waterValue = value; }
    #endregion
}
