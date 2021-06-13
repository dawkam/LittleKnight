using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class VillagerAgent : Agent, IObserver
{
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
    private GameObject warehouse;
    private GameObject well;

    public override void Initialize()
    {
        base.Initialize();
        rigidbody = GetComponent<Rigidbody>();
        parametersGiver = FindObjectOfType<ParametersGiver>();
        warehouse = GameObject.FindGameObjectWithTag("Warehouse");
        well = GameObject.FindGameObjectWithTag("Well");
        if (parametersGiver == null || warehouse == null || well == null)
            Debug.LogError("Parameters giver or well or warehoise is missing!!");
    }

    public override void OnEpisodeBegin()
    {
        ResetData();
        StartCoroutine(ExistancePunishment());
        StartCoroutine(StaminaRegeration());
    }
    private void ResetData()
    {
        hungerCurrent = parametersGiver.HungerMax;
        thirstCurrent = parametersGiver.ThirstMax;

        staminaCurrent = parametersGiver.StaminaMax;
        moveSpeedCurrent = parametersGiver.MoveSpeedMax;

        isControlEnabled = true;
        isAlive = true;
    }

    /// <summary>
    /// Perform actions based on a vector of numbers
    /// </summary>
    /// <param name="vectorAction">The list of actions to take</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (isControlEnabled)
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

            // Apply movement
            Move(transform.position + transform.forward * forwardAmount * moveSpeedCurrent * Time.fixedDeltaTime);
            //rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeedCurrent * Time.fixedDeltaTime);
            transform.Rotate(transform.up * turnAmount * parametersGiver.TurnSpeed * Time.fixedDeltaTime);
        }

        //if (actions.DiscreteActions[2] == 1)
        //    Eat();


        // Apply a tiny negative reward every step to encourage action
        //if (MaxStep > 0) AddReward(-1f / MaxStep);
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

        if (Input.GetKey(KeyCode.E))
        {
            // eat
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

        // Direction to well (1 Vector3 = 3 values)
        sensor.AddObservation((well.transform.position - transform.position).normalized);  
        
        // Direction to warehouse (1 Vector3 = 3 values)
        sensor.AddObservation((warehouse.transform.position - transform.position).normalized);

        // Direction villager is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward);

        // Whether the villager is hungry (1 float = 1 value)
        sensor.AddObservation(hungerCurrent);

        // Whether the villager is thirsty (1 float = 1 value)
        sensor.AddObservation(thirstCurrent);

        // Whether the villager is tired (1 float = 1 value)
        sensor.AddObservation(staminaCurrent);  
        
        // Whether the villager is tired (1 float = 1 value)
        sensor.AddObservation(restTimeCurrent);

        //// 1 + 1 + 1 + 3 + 3 + 3 + 1 + 1 + 1 + 1 +  = 16 total values
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
            EatFromWarehouse();
        }
        else if (collision.transform.CompareTag("Well"))
        {
            Drink();
        }
    }

    #region Simple action

    private void Move(Vector2 movement)
    {
        if (staminaCurrent > 0)
        {
            rigidbody.MovePosition(movement);
            // staminaCurrent -= (parametersGiver.StaminaTick * foodCount);
        }
        else if (staminaCurrent == 0)
        {
            staminaCurrent = parametersGiver.StaminaMax;
            StartCoroutine(Tiredness());
        }
    }

    private void Eat(GameObject collectable)
    {
        //collectorArea.RemoveSpecificCollectable(collectable);
        hungerCurrent += parametersGiver.FoodValue;
        AddReward(0.5f);
    }

    private void EatFromWarehouse()
    {
        //!!!!!! usuwanie z magazynu!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        hungerCurrent += parametersGiver.FoodValue;
        AddReward(1f);
    }

    private void Drink()
    {
        thirstCurrent += parametersGiver.WaterValue;
        AddReward(1f);
    }

    public void Die()
    {
        isAlive = false;
        AddReward(-1f);
        StopAllCoroutines();
        EndEpisode();
    }
    #endregion

    #region Coroutine
    IEnumerator Tiredness()
    {
        isControlEnabled = false;
        while (restTimeCurrent > 0)
        {
            restTimeCurrent--;
            yield return null;
        }
        isControlEnabled = true; ;
    }

    IEnumerator ExistancePunishment()
    {
        while (isAlive && hungerCurrent > 0 && thirstCurrent > 0)
        {
            hungerCurrent -= parametersGiver.HungerTick;
            AddReward(-1f / hungerCurrent);

            thirstCurrent -= parametersGiver.ThirstTick;
            AddReward(-1f / thirstCurrent);

            yield return null;
        }
        Die();
    }

    IEnumerator StaminaRegeration()
    {
        while (isAlive)
        {
            if (staminaCurrent < parametersGiver.StaminaMax)
                staminaCurrent += parametersGiver.StaminaTick;
            yield return null;
        }
    }

    public void UpdateObserver()
    {
        //comfort = village.comfort;!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    #endregion
}


// To Do:
// 
// 2.Ogarniecie vectora obserwacji x
// 3. Mechanika komfortu 
// 4. Dodanie areny nadrzędnej
// 6. dash