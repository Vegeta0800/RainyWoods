using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods;

public abstract class Mechanic : ScriptableObject
{
    //Declare variables needed in all mechanics.
    public float duration;
    public float delayByAnimation;
    public float cooldown;
    public float maxAmount;
    public bool cleanUp;
    public float amount;
    public bool useable = true;

    /// <summary>
    /// Declare an overrideable method for executing all mechanics. 
    /// </summary>
    /// <param name="playerMechanics"></param>
    public abstract void ExecuteMechanic(PlayerMechanics playerMechanics);

    /// <summary>
    /// Declare an overrideable method for cleaning up all mechanics. 
    /// </summary>
    /// <param name="playerMechanics"></param>
    public abstract void CleanUpMechanic(PlayerMechanics playerMechanics);
}
