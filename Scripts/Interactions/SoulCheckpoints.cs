using System.Collections.Generic;
using UnityEngine;
using RainyWoods;

public class SoulCheckpoints : Interaction
{
    //Variables needed for this interaction
    public List<Transform> soulPoints;
    private List<Soul> collectedSouls = new List<Soul>();
    private int index = 0;

    /// <summary>
    /// Define the overrideable Interact. Decides if it should get a soul or set a soul.
    /// if player has no souls or if the capacity of this soulcheckpoint is reached give all souls that are stored to the player.
    /// if not take 1 soul of the player if possible.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(Player player)
    {
        if (index >= soulPoints.Count || player.gate.collectedSouls.Count == 0)
                GetSoul(player);
        else if (player.gate.soulsCollected > 0 && collectedSouls.Count < soulPoints.Count)
                SetSoul(player);
    }

    /// <summary>
    /// make the soul move to the position on the checkpoint.
    /// set all soul variables that are needed for this action.
    /// enable all collisions.
    /// </summary>
    /// <param name="player"></param>
    private void SetSoul(Player player)
    {
        Soul soul = player.gate.collectedSouls[player.gate.collectedSouls.Count - 1].GetComponent<Soul>();

        foreach (Collider c in soul.GetComponents<Collider>())
        {
            c.enabled = true;
        }

        player.gate.collectedSouls.RemoveAt(player.gate.collectedSouls.Count - 1);
        player.gate.soulsCollected--;

        soul.soulMovement.moveTransform = soulPoints[index];
        soul.soulMovement.onPlayer = false;
        soul.soulMovement.collected = false;
        soul.soulMovement.onCheckpoint = true;
        soul.soulMovement.moveTowardsObject = true;
        soul.transform.SetParent(null);
        soul.soulMovement.moveToCheckpoint = true;
        soul.transform.rotation = Quaternion.Euler(0, 0, 0);
        collectedSouls.Add(soul);
        index++;
    }

    /// <summary>
    /// Get all souls of the player and set needed variables for the taken soul.
    /// Afterwards reset everything and clear the collectedSouls list.
    /// </summary>
    /// <param name="player"></param>
    private void GetSoul(Player player)
    {
        if (collectedSouls.Count == 0 && player.gate.collectedSouls.Count == 0)
            return;

        foreach (Soul soul in collectedSouls)
        {
            soul.soulMovement.onPlayer = false;
            soul.soulMovement.collected = false;
            soul.soulMovement.playerHit = false;
            soul.soulMovement.moveTowardsObject = true;
            soul.soulMovement.moveToGate = false;
            soul.soulMovement.onCheckpoint = false;
            soul.soulMovement.moveTransform = player.transform;
            index--;
        }
                collectedSouls.Clear();
    }

    /// <summary>
    /// if the soul collides if this object, the soul should stop moving.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Soul"))
        {
            collision.collider.GetComponent<Soul>().soulMovement.moveTowardsObject = false;
        }
    }
}
