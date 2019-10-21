using UnityEngine;


public abstract class AIDecision : ScriptableObject
{
    /// <summary>Declare an overrideable method for all Decisions. Returns a bool. 
    /// <param> AIController controller
    /// </summary
    public abstract bool Decide(AIController controller);
}
