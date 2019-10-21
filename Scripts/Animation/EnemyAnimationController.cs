using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : BaseAnimationController
{
    public string pickupTriggerParameter = "OnPickup";
    public AIController aiController;

    new void Reset()
    {
        base.Reset();

        aiController = GetComponent<AIController>();
    }

    new void Start()
    {
        base.Start();

        if (!aiController)
            throw new MissingReferenceException("AI Controller is required but missing");
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        float coffeeLevel = aiController.coffeeMeter / aiController.maxCoffeeMeter;
        animationController.SetFloat("CoffeeLevel", coffeeLevel);
    }

    public void OnPickup()
    {
        animationController.SetTrigger(pickupTriggerParameter);
    }
}
