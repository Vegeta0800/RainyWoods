using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ScanDecision")]
public class ScanDecision : AIDecision
{
    /// <summary>Define the overrideable Decide method to return the result of Scan.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        return Scan(controller);
    }

    /// <summary>If the player isnt caught yet shoot a spherecast (controller.viewRadius wide && controller.viewRange far)
    /// if an object with the tag Player is found return true and set chaseTarget to the player transform.
    /// if not return false.
    /// <param> AIController controller
    /// </summary
    private bool Scan(AIController controller)
    {
        if (!controller.player.caught && !controller.player.disguised && controller.arrived)
        {
            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.viewRange, Color.green);

            RaycastHit hit;

            if (Physics.SphereCast(controller.eyes.position, controller.viewRadius, controller.eyes.forward, out hit, controller.viewRange) && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
