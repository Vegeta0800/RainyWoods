using System.Collections;
using System.Collections.Generic;
using RainyWoods;
using UnityEngine;
using UTools.Engine.Extensions.ClassExtensions;

[CreateAssetMenu(menuName = "Mechanics/SpawnCoffee")]
public class SpawnCoffee : Mechanic
{
    //Variables needed for this mechanic.
    [SerializeField] private GameObject coffee = null;
    private GameObject tempCoffee;
    private int i = 0;

    /// <summary>
    ///  Define the overrideable CleanUpMechanic. Reset the setup for the mechanic.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void CleanUpMechanic(PlayerMechanics playerMechanics)
    {
        i = 0;
    }

    /// <summary>
    ///  Define the overrideable ExecuteMechanic.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void ExecuteMechanic(PlayerMechanics playerMechanics)
    {
        Spawn(playerMechanics);
    }

    /// <summary>
    /// if this is the first time called while activated, spawn a coffee infront of the player. All Coffee Values are set on the prefab included.
    /// </summary>
    /// <param name="playerMechanics"></param>
    private void Spawn(PlayerMechanics playerMechanics)
    {
        if (i == 0)
        {
            tempCoffee = null;
            tempCoffee = Instantiate(coffee, (playerMechanics.player.transform.position + playerMechanics.player.transform.forward.Multiply(playerMechanics.player.coffeeOffset)), Quaternion.Euler(-90, 0, 0));
            i++;
        }
    }
}
