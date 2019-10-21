using RainyWoods;
using UnityEngine;

[CreateAssetMenu(menuName = "Mechanics/CoffeeBomb")]
public class CoffeeBomb : Mechanic
{
    [SerializeField] private GameObject coffeeBombParticle = null;

    private GameObject iBomb;

    private int i = 0;

    /// <summary>
    ///  Define the overrideable CleanUpMechanic. Reset the particle for the coffeeBomb.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void CleanUpMechanic(PlayerMechanics playerMechanics)
    {
        i = 0;
        iBomb = null;
        playerMechanics.player.transform.position = playerMechanics.player.teleportPoint.position;
        playerMechanics.player.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    /// <summary>
    ///  Define the overrideable ExecuteMechanic. Play the coffeeBomb particle effect if it isnt null.
    ///  Afterwards, let AI lose the focus of the player and teleport the player towards the closet checkpoint/spawnpoint.
    /// </summary>
    /// <param name="playerMechanics"></param>
    public override void ExecuteMechanic(PlayerMechanics playerMechanics)
    {
        if (i == 0)
        {
            playerMechanics.player.caught = true;
            iBomb = Instantiate(coffeeBombParticle, playerMechanics.player.transform.position, Quaternion.identity, null);
            if (iBomb && !iBomb.activeInHierarchy)
            {
                iBomb.SetActive(true);
            }
            i++;
        }
    }
}
