using System.Collections;
using System.Collections.Generic;
using RainyWoods;
using UnityEngine;

[CreateAssetMenu(menuName = "Mechanics/Espresso")]
public class Espresso : Mechanic
{
    //Variables needed for this mechanic.
    [SerializeField] private float espressoValue = 2;
    private float originalSpeed;
    private int i = 0;

    /// <summary>
    ///  Define the overrideable CleanUpMechanic. Reset the setup for the mechanic and reset the maxSpeed.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void CleanUpMechanic(PlayerMechanics playerMechanics)
    {
        playerMechanics.player.maxSpeed = originalSpeed;
        i = 0;
    }

    /// <summary>
    ///  Define the overrideable ExecuteMechanic. Save the original maxSpeed for the reset and then exponentially increase the maxSpeed. 
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void ExecuteMechanic(PlayerMechanics playerMechanics)
    {
        if (i == 0)
        {
            originalSpeed = playerMechanics.player.maxSpeed;
            i++;
        }

        playerMechanics.player.maxSpeed *= (1 + (espressoValue / 1000.0f));
    }
}
