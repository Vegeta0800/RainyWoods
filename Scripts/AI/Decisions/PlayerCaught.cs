using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/PlayerCaught")]
public class PlayerCaught : AIDecision
{
    /// <summary>Define the overrideable Decide method to return if the player was caught or not.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.player.transform.position) > controller.loseRange)
        {
            return true;
        }

        return controller.player.caught;
    }
}
