using UnityEngine;

public class Coffee : MonoBehaviour
{
    /// <summary>
    /// if the Enemy walks into the coffee trigger, the coffee is in range to be drunken.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AIController>().coffeeInRange = true; 
            other.GetComponent<AIController>().coffee = this.gameObject;
        }
    }

    /// <summary>
    /// if the enemy exits the coffee trigger, the coffee isnt in range anymore.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AIController>().coffeeInRange = false;
        }
    }
}
