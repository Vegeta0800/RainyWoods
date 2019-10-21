using UnityEngine;
using RainyWoods;

[CreateAssetMenu(menuName = "AI/Actions/Drink")]
public class DrinkCoffee : AIAction
{
    //Variables needed for this action.
    [SerializeField] private float drinkinTime = 2.0f;
    [SerializeField] private float drinkingValue = 0.5f;
    private bool waiting = false;
    private float waitTime = 0.0f;
    int i = 0;

    /// <summary>
    /// Override Act function from parent class Action.
    /// if this is the first time this function is called, setup the AI for drinking coffee.
    /// </summary>
    /// <param name="controller"></param>
    public override void Act(AIController controller)
    {
        if (i == 0 && controller.coffee != null)
        {
            waiting = false;
            waitTime = drinkinTime;
            controller.coffeeDrank = false;
            controller.navMeshAgent.destination = controller.coffee.transform.position;
            controller.navMeshAgent.isStopped = false;
            i++;

        }
        Drink(controller);     
    }

    /// <summary>while the AI isnt infront of the coffee, move towards the coffee
    /// if coffee is reached, wait for a cooldown and then destroy the coffee, set coffee related variables, lower the coffeeMeter and reset the setup in the Act function.
    /// <param> AIController controller
    /// </summary>
    private void Drink(AIController controller)
    {
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            if (waiting)
            {
                waiting = false;
                controller.coffeeDrank = true;
                controller.coffeeMeter -= drinkingValue;
                controller.navMeshAgent.isStopped = false;
                if(controller.coffee.gameObject != null)
                    Destroy(controller.coffee.gameObject);
                controller.coffeeInRange = false;
                controller.coffee = null;
                i = 0;

                controller.GetComponent<EnemyAnimationController>()?.OnPickup();
            }
            else
            {
                if (waitTime >= 0.0f)
                {
                    waitTime -= 1.0f * Time.deltaTime;
                }
                else
                {
                    waiting = true;
                }
            }
        }
        else
        {
            if (controller.coffee.gameObject != null)
                controller.navMeshAgent.destination = controller.coffee.gameObject.transform.position;
            controller.navMeshAgent.isStopped = false;
        }
    }
}
