using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chasing")]
public class Chasing : AIAction
{
    /// <summary>Define the overrideable Act method to Chase.
    /// <param> AIController controller
    /// </summary
    public override void Act(AIController controller)
    {
        Chase(controller);
    }

    /// <summary>Sets the NavMeshAgent destination to the chaseTarget inside of AIController and increase speed.
    /// <param> AIController controller
    /// </summary
    private void Chase(AIController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.speed = controller.chaseSpeed;  
        controller.navMeshAgent.isStopped = false;
    }
}
