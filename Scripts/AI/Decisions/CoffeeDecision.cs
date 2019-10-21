using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods;

[CreateAssetMenu(menuName = "AI/Decision/Coffee")]
public class CoffeeDecision : AIDecision
{
    /// <summary>Define the overrideable Decide method to return if coffee is in range to drink or not.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        return controller.coffeeInRange;
    }
}
