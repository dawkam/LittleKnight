using UnityEngine;
using Unity.MLAgents;
using System;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CollectorAgent : Agent
{

    [Tooltip("How fast the agent moves forward")]
    public float moveSpeed = 5f;
    private float currentMoveSpeed;

    [Tooltip("How fast the agent turns")]
    public float turnSpeed = 180f;

    [Tooltip("Prefab of the heart that appears when the baby is fed")]
    public GameObject heartPrefab;

    [Tooltip("Prefab of the regurgitated fish that appears when the baby is fed")]
    public GameObject regurgitatedFishPrefab;

    private CollectorArea collectorArea;
    new private Rigidbody rigidbody;
    private GameObject baby;
    //private bool isFull; // If true, penguin has a full stomach
    private bool isAlive;
    private int collected = 0;

    private float feedRadius = 0f;

    /// <summary>
    /// Initial setup, called when the agent is enabled
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        collectorArea = GetComponentInParent<CollectorArea>();
        baby = collectorArea.baby;
        rigidbody = GetComponent<Rigidbody>();
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

        // Apply movement
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * currentMoveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        // Apply a tiny negative reward every step to encourage action
        if (MaxStep > 0) AddReward(-1f / MaxStep);
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
    }

    /// <summary>
    /// Reset the agent and area
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //isFull = false;
        isAlive = true;
        collected = 0;
        currentMoveSpeed = moveSpeed;
        collectorArea.ResetArea();
        feedRadius = Academy.Instance.EnvironmentParameters.GetWithDefault("feed_radius", 0f);
    }

    /// <summary>
    /// Collect all non-Raycast observations
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)
    {
        // Whether the penguin has eaten a fish (1 float = 1 value)
        // sensor.AddObservation(isFull);
        sensor.AddObservation(collected);

        // Whether the penguin is alive (1 float = 1 value)
        sensor.AddObservation(isAlive);

        // Distance to the baby (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(baby.transform.position, transform.position));

        // Direction to baby (1 Vector3 = 3 values)
        sensor.AddObservation((baby.transform.position - transform.position).normalized);

        // Direction penguin is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward);


        // 1 + 1 + 1 + 3 + 3 = 9 total values
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

        // Test if the agent is close enough to to feed the baby
        if (Vector3.Distance(transform.position, baby.transform.position) < feedRadius)
        {
            // Close enough, try to feed the baby
            GiveCollectable();
        }
    }

    /// <summary>
    /// When the agent collides with something, take action
    /// </summary>
    /// <param name="collision">The collision info</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Collectable"))
        {
            // Try to eat the fish
            Collect(collision.gameObject);
        }
        else if (collision.transform.CompareTag("Baby"))
        {
            // Try to feed the baby
            GiveCollectable();
        }
    }
    /// <summary>
    /// Check if agent is full, if not, eat the fish and get a reward
    /// </summary>
    /// <param name="collectable">The fish to eat</param>
    private void Collect(GameObject collectable)
    {
        //if (isFull) return; // Can't eat another fish while full
        //isFull = true;

        collectorArea.RemoveSpecificCollectable(collectable);
        currentMoveSpeed--;
        collected++;
        AddReward(1f);
    }

    /// <summary>
    /// Check if agent is full, if yes, feed the baby
    /// </summary>
    private void GiveCollectable()
    {
        //if (!isFull) return; // Nothing to regurgitate
        //isFull = false;

        // Spawn regurgitated fish
        GameObject regurgitatedFish = Instantiate<GameObject>(regurgitatedFishPrefab);
        regurgitatedFish.transform.parent = transform.parent;
        regurgitatedFish.transform.position = baby.transform.position;
        Destroy(regurgitatedFish, 4f);

        // Spawn heart
        GameObject heart = Instantiate<GameObject>(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.position = baby.transform.position + Vector3.up;
        Destroy(heart, 4f);

        AddReward(2f * collected);
        collected = 0;
        currentMoveSpeed = moveSpeed;

        if (collectorArea.CollectableCount <= 0)
        {
            EndEpisode();
        }
    }
    public void Die()
    {
        isAlive = false;
        AddReward(-1f);
        EndEpisode();
    }

}