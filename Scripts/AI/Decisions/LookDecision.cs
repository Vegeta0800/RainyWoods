using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Look")]
public class LookDecision : AIDecision
{
    /// <summary>Define the overrideable Decide method to return the result of Look.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        return Look(controller);
    }

    /// <summary>If the player isnt caught yet shoot a spherecast (controller.viewRadius wide && controller.viewRange far)
    /// if an object with the tag Player is found return true and set the last seen position to the players current position.
    /// if not return false. 
    /// <param> AIController controller
    /// </summary
    private bool Look(AIController controller)
    {
        if (!controller.player.caught && !controller.player.disguised)
        {
            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.viewRange, Color.green);

            RaycastHit hit;

            if (Physics.SphereCast(controller.eyes.position, controller.viewRadius, controller.eyes.forward, out hit, controller.viewRange) && hit.collider.CompareTag("Player") ||
                Vector3.Distance(controller.transform.position, controller.player.transform.position) <= controller.spotRadius)
            {
                controller.chaseTarget = controller.player.transform;
                return true;
            }
            else
            {
                controller.chaseTarget = null;
                return false;
            }
        }

        controller.chaseTarget = null;
        return false;
    }
}
