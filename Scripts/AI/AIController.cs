using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RainyWoods;

public class AIController : MonoBehaviour
{
    //States
    public State currentState;
    public Player player;


    //Public variables for the different scripts
    public List<Transform> patrolPointList;
    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public bool enabledAI = true;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float coffeeMeter = 1.0f;
    [HideInInspector] public bool coffeeDrank = false;
    [HideInInspector] public bool coffeeInRange = false;
    [HideInInspector] public bool finishedScanning = false;
    [HideInInspector] public bool arrived = false;
    [HideInInspector] public GameObject coffee = null;
    [HideInInspector] public Vector3 lastSeenPos = new Vector3(0, 0, 0);

    //ReaperStats
    [Header("Reaper Stats")]
    public Transform eyes;
    public float viewRadius;
    public float viewRange;
    public float speed;
    public float chaseSpeed;
    public float maxSpeed;
    public float maxCoffeeMeter;
    public float maxViewRange;
    public float spotRadius;
    public float loseRange;

    [Range(1.0f, 600.0f)]
    public float coffeeLossTime = 300.0f;
    public float CoffeePerFrame => ((maxCoffeeMeter - 1.0f) * Time.deltaTime) / coffeeLossTime;

    /// <summary>Set NavMeshAgent and coffeeMeter
    /// </summary>
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        coffeeMeter = 1.0f;
    }
    /// <summary>Update the State of the AI in State.UpdateState() if the AI is enabled. Also Update the values with coffeeMeter.
    /// </summary>
    private void Update()
    {
        if (enabledAI)
        {
            currentState.UpdateState(this);

            navMeshAgent.speed = speed;
            speed *= coffeeMeter;
            speed = Mathf.Clamp(speed, 1.0f, maxSpeed);

            //coffeeMeter += 0.000083f;
            coffeeMeter += CoffeePerFrame;
            coffeeMeter = Mathf.Clamp(coffeeMeter, 1.0f, maxCoffeeMeter);

            //viewRange *= coffeeMeter;
            //viewRange = Mathf.Clamp(viewRange, 1.0f, maxViewRange);

        }
    }

    /// <summary>Transition to the next State if its not the remain State.
    /// <param> State nextState
    /// </summary>
    public void TransitionState(State nextState)
    {
        currentState = nextState;
    }

}

