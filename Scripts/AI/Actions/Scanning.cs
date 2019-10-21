using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Scanning")]
public class Scanning : AIAction
{
    //Variables needed for this action
    public float rotateSpeed;

    private int i = 0;
    private float t = 0.0f;
    private float startRotation;
    private float endRotation;

    /// <summary>Define the overrideable Act method to Scan.
    /// <param> AIController controller
    /// </summary
    public override void Act(AIController controller)
    {
        Scan(controller);
    }

    /// <summary>Move towards the the last seen player position.
    /// When reached, setup some stats the first time the function is called and then proceed, to rotate 360 around yourself and scan the area for the enemy.
    /// <param> AIController controller
    /// </summary
    private void Scan(AIController controller)
    {
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            if (i == 0)
            {
                t = 0.0f;
                startRotation = controller.transform.eulerAngles.y;
                endRotation = startRotation + 360.0f;
                controller.finishedScanning = false;
                controller.arrived = false;
                controller.chaseTarget = null;
                i++;
            }

            if (t < rotateSpeed)
            {
                controller.arrived = true;
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotateSpeed) % 360.0f;
                controller.transform.eulerAngles = new Vector3(controller.transform.eulerAngles.x, yRotation, controller.transform.eulerAngles.z);
            }
            else
            {
                controller.arrived = false;
                controller.finishedScanning = true;
                t = 0.0f;
            }
        }
        else
        {
            controller.navMeshAgent.destination = controller.lastSeenPos;
            controller.navMeshAgent.isStopped = false;
        }
    }
}
