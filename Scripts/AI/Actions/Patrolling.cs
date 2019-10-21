using UnityEngine;
using RainyWoods;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class Patrolling : AIAction
{
    //Variables needed for this action.
    public float patrolCooldown;
    private float waitTime = 0.0f;
    private bool waiting = false;

    public float newRotY;
    public float newRotYN;

    /// <summary>
    /// Override Act function from parent class Action.
    /// </summary>
    /// <param name="controller"></param>
    public override void Act(AIController controller)
    {
        Patrol(controller);
    }

    /// <summary>Setting the destination of the NavMeshAgent to the next PatrolPoint from the AIController. Wait for patrolCooldown seconds if patrolPoint is reached.
    /// In said patrolCooldown, turn a little bit to left and right to look around a little bit.
    /// Set the AI speed to the normal speed
    /// <param> AIController controller
    /// </summary>
    private void Patrol(AIController controller)
    {
        controller.arrived = false;
        controller.chaseTarget = null;

        if (!waiting)
        {
            controller.navMeshAgent.speed = controller.speed;
            waitTime = patrolCooldown;
            controller.navMeshAgent.destination = controller.patrolPointList[controller.nextPatrolPoint].position;
            controller.navMeshAgent.isStopped = false;

            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
            {
                waiting = true;
                controller.nextPatrolPoint = (controller.nextPatrolPoint + 1) % controller.patrolPointList.Count;
                controller.navMeshAgent.isStopped = true;
                if(controller.player.caught)
                    controller.player.caught = false;
            }
        }
        else
        {
            if (waitTime >= 0.0f)
            {
                waitTime -= 1.0f * Time.deltaTime;

                if (waitTime < patrolCooldown * 0.5f)
                {
                    //controller.transform.Rotate(new Vector3(0, newRotY * Time.deltaTime, 0));
                }
                else if (waitTime > patrolCooldown * 0.5f)
                {
                    //controller.transform.Rotate(new Vector3(0, (newRotYN * Time.deltaTime), 0));
                }
            }
            else
            {
                waiting = false;
            }
        }
    }
}
