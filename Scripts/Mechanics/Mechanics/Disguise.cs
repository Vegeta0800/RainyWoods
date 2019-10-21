using RainyWoods;
using UnityEngine;

[CreateAssetMenu(menuName = "Mechanics/Disguise")]
public class Disguise : Mechanic
{
    /// <summary>
    ///  Define the overrideable CleanUpMechanic. Set player.disguised to false.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void CleanUpMechanic(PlayerMechanics playerMechanics)
    {
        playerMechanics.player.disguised = false;
    }

    /// <summary>
    ///  Define the overrideable ExecuteMechanic. Set player.disguised to true.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void ExecuteMechanic(PlayerMechanics playerMechanics)
    {
        playerMechanics.player.disguised = true;
    }
}
