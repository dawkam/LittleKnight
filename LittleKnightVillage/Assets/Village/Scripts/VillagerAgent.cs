using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class VillagerAgent : Agent, IObserver
{
    [SerializeField] private bool isTraining = true;

    #region Currrent data
    private float hungerCurrent;
    private float thirstCurrent;
    private float staminaCurrent;
    private float restTimeCurrent;

    private float comfort = 1f;

    private float moveSpeedCurrent;

    private bool isAlive;
    private bool isControlEnabled;
    #endregion

    private ParametersGiver parametersGiver;
    new private Rigidbody rigidbody;
    private Warehouse warehouse;
    private GameObject well;
    [SerializeField] private VillageArea villageArea;
    [SerializeField] private Village village;

    private readonly int rewardModifiaer = 1;

    public float HungerCurrent { get => hungerCurrent; private set => hungerCurrent = value; }
    public float ThirstCurrent { get => thirstCurrent; private set => thirstCurrent = value; }
    public float StaminaCurrent { get => staminaCurrent; private set => staminaCurrent = value; }
    public float RestTimeCurrent { get => restTimeCurrent; private set => restTimeCurrent = value; }

    public override void Initialize()
    {
        base.Initialize();
        InitializeData();

        if (parametersGiver == null || warehouse == null || well == null)
            Debug.LogError("Parameters giver or well or warehouse is missing!!");
    }

    private void InitializeData()
    {
        rigidbody = GetComponent<Rigidbody>();
        parametersGiver = FindObjectOfType<ParametersGiver>();
        warehouse = FindObjectOfType<Warehouse>();
        well = GameObject.FindGameObjectWithTag("Well");
        village = FindObjectOfType<Village>();
        village.Attach(this);
    }

    public override void OnEpisodeBegin()
    {
        ResetData();
        StartCoroutine(ExistancePunishment());
        StartCoroutine(StaminaRegeration());
    }
    private void ResetData()
    {
        HungerCurrent = parametersGiver.HungerMax;
        ThirstCurrent = parametersGiver.ThirstMax;

        StaminaCurrent = parametersGiver.ComfortMin;
        moveSpeedCurrent = parametersGiver.MoveSpeedMax;

        isControlEnabled = true;
        isAlive = true;
        village.Reset();
        villageArea.ResetArea();
    }

    /// <summary>
    /// Perform actions based on a vector of numbers
    /// </summary>
    /// <param name="vectorAction">The list of actions to take</param>
    public override void OnActionReceived(ActionBuffers actions)
    {

        // Convert the first action to forward movement
        float forwardAmount = actions.DiscreteActions[0];

        // Convert the second action to turning left or right
        float turnAmount = 0f;
        if (actions.DiscreteActions[1] == 1f)
        {
            turnAmount = -1f;
        }
        else if (actions.DiscreteActions[1] == 2f)
        {
            turnAmount = 1f;
        }

        bool isDash = actions.DiscreteActions[2] == 1f;


        ApplyMovement(forwardAmount, turnAmount, isDash);


        //if (actions.DiscreteActions[2] == 1)
        //    Eat();


        // Apply a tiny negative reward every step to encourage action
        //if (MaxStep > 0) AddReward(-1f / MaxStep);
    }

    private void ApplyMovement(float forwardAmount, float turnAmount, bool isDash)
    {
        if (isControlEnabled)
        {
            if (StaminaCurrent > 0)
            {
                if (isDash)
                    Dash();
                Move(forwardAmount, turnAmount);
            }
            else
            {
                StopCoroutine(Tiredness());
                StartCoroutine(Tiredness());
            }
        }
    }

    /// <summary>
    /// Read inputs from the keyboard and convert them to a list of actions.
    /// This is called only when the player wants to control the agent and has set
    /// Behavior Type to "Heuristic Only" in the Behavior Parameters inspector.
    /// </summary>
    /// <returns>A vectorAction array of floats that will be passed into <see cref="AgentAction(float[])"/></returns>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Array.Clear(actionsOut.DiscreteActions.Array, 0, actionsOut.DiscreteActions.Length);
        if (Input.GetKey(KeyCode.W))
        {
            // move forward
            actionsOut.DiscreteActions.Array[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // turn left
            actionsOut.DiscreteActions.Array[1] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // turn right
            actionsOut.DiscreteActions.Array[1] = 2;
        }
        if (Input.GetKey(KeyCode.X))
        {
            //dash
            actionsOut.DiscreteActions.Array[2] = 1;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Whether the villager is alive (1 float = 1 value)
        sensor.AddObservation(isAlive);

        // Distance to the well (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(well.transform.position, transform.position));

        // Distance to the warehouse (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(warehouse.transform.position, transform.position));

        // Food count in warehouse (1 float = 1 value)
        sensor.AddObservation(warehouse.foodCount);

        // Direction to well (1 Vector3 = 3 values)
        sensor.AddObservation((well.transform.position - transform.position).normalized);

        // Direction to warehouse (1 Vector3 = 3 values)
        sensor.AddObservation((warehouse.transform.position - transform.position).normalized);

        // Direction villager is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward);

        // Whether the villager is hungry (1 float = 1 value)
        sensor.AddObservation(HungerCurrent);

        // Whether the villager is thirsty (1 float = 1 value)
        sensor.AddObservation(ThirstCurrent);

        // Whether the villager is tired (1 float = 1 value)
        sensor.AddObservation(StaminaCurrent);

        // Whether the villager is tired (1 float = 1 value)
        sensor.AddObservation(RestTimeCurrent);

        //// 1 + 1 + 1 + 1 + 3 + 3 + 3 + 1 + 1 + 1 + 1 +  = 17 total values
    }

    private void FixedUpdate()
    {
        // Request a decision every 5 steps. RequestDecision() automatically calls RequestAction(),
        // but for the steps in between, we need to call it explicitly to take action using the results
        // of the previous decision
        if (StepCount % 5 == 0)
        {
            RequestDecision();
        }
        else
        {
            RequestAction();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Food"))
        {
            Eat(collision.gameObject);
        }
        else if (collision.transform.CompareTag("Warehouse"))
        {
            Warehouse warehouse = collision.gameObject.GetComponent<Warehouse>();
            EatFromWarehouse(warehouse);
        }
        else if (collision.transform.CompareTag("Well"))
        {
            Drink();
        }
    }

    #region Simple action

    private void Move(float forwardAmount, float turnAmount)
    {
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeedCurrent * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * parametersGiver.TurnSpeed * Time.fixedDeltaTime);
        moveSpeedCurrent = parametersGiver.MoveSpeedMax;
    }

    private void Dash()
    {
        staminaCurrent -= parametersGiver.StaminaDashTick;
        moveSpeedCurrent = parametersGiver.DashPower;
    }
    private void Eat(GameObject collectable)
    {
        if (villageArea != null)
            villageArea.RemoveSpecificCollectable(collectable);
        else
            Destroy(collectable);

        if (HungerCurrent + parametersGiver.FoodValue < parametersGiver.HungerMax)
        {
            HungerCurrent = parametersGiver.FoodValue;
            AddReward(0.5f);
        }
    }

    private void EatFromWarehouse(Warehouse warehouse)
    {
        if (HungerCurrent + parametersGiver.FoodValue < parametersGiver.HungerMax && warehouse.TakeFood())
        {
            HungerCurrent = parametersGiver.HungerMax;
            AddReward(1f);
        }
    }

    private void Drink()
    {
        if (ThirstCurrent + parametersGiver.WaterValue < parametersGiver.ThirstMax)
        {
            ThirstCurrent = parametersGiver.ThirstMax;
            AddReward(1f);
        }
    }

    public void Die()
    {
        isAlive = false;
        village.Detach(this);
        AddReward(-1f);
        StopAllCoroutines();

        if (isTraining)
            EndEpisode();
        else
            Destroy(this.gameObject);
    }
    #endregion

    #region Coroutine
    IEnumerator Tiredness()
    {
        isControlEnabled = false;
        RestTimeCurrent = parametersGiver.RestTime;
        while (RestTimeCurrent > 0)
        {
            RestTimeCurrent--;
            yield return new WaitForSeconds(1f);
        }
        isControlEnabled = true;
    }

    IEnumerator ExistancePunishment()
    {
        while (isAlive)
        {
            HungerCurrent -= parametersGiver.HungerTick;
            if (HungerCurrent > 0)
                AddReward(-1f / (HungerCurrent * rewardModifiaer));
            else break;

            ThirstCurrent -= parametersGiver.ThirstTick;
            if (ThirstCurrent > 0)
                AddReward(-1f / (ThirstCurrent * rewardModifiaer));
            else break;

            yield return new WaitForSeconds(1f);
        }
        Die();
    }

    IEnumerator StaminaRegeration()
    {
        while (isAlive)
        {
            if (StaminaCurrent < comfort)
                StaminaCurrent += parametersGiver.StaminaTick;
            yield return null;
        }
    }

    public void UpdateObserver()
    {
        comfort = village.Comfort;
    }

    #endregion
}


// To Do:
// 
// 2.Ogarniecie vectora obserwacji x
// 3. Mechanika komfortu x
// 4. Dodanie areny nadrzędnej x
// 6. dash x