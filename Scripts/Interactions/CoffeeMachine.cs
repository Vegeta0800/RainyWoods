using System.Collections;
using System.Collections.Generic;
using RainyWoods;
using UnityEngine;

public class CoffeeMachine : Interaction
{
    //Variables needed for this Interaction.

    /// <summary>
    /// Define the overrideable Interact. Resets the amount of all mechanics inside player.mechanics if available.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(Player player)
    {
        if(player.mechanics[0].amount < player.mechanics[0].maxAmount)
        {
            player.mechanics[0].amount = player.mechanics[0].maxAmount;
            //INSERT SFX
        }
    }
}
