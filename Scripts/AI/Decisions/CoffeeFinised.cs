using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/CoffeeFinished")]
public class CoffeeFinised : AIDecision
{
    /// <summary>Define the overrideable Decide method to return if the coffee has been drunk or not.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        return controller.coffeeDrank;
    }

}
