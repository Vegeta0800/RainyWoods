using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAnimationController : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent aiAgent;
    public Rigidbody sourceRigidbody;
    public Animator animationController;

    [Header("Animation Parameters")]
    public string velocityParameter = "LastVelocity";

    protected void Reset()
    {
        aiAgent = GetComponent<NavMeshAgent>();
        sourceRigidbody = GetComponent<Rigidbody>();
        animationController = GetComponent<Animator>();
    }

    protected void Start()
    {
        if (!aiAgent && !sourceRigidbody)
            throw new MissingReferenceException("Either a Rigidbody or NavMeshAgent is required but missing");

        if (!animationController)
            throw new MissingReferenceException("Animation Controller is required but not assigned");
    }

    protected void Update()
    {
        float velocity = 0.0f;
        if (aiAgent)
            velocity = aiAgent.velocity.magnitude;
        else
            velocity = sourceRigidbody.velocity.magnitude;

        animationController.SetFloat(velocityParameter, velocity);
    }
}
