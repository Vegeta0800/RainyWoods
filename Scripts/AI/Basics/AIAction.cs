using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    /// <summary>Declare an overrideable method for all Actions. 
    /// <param> AIController controller
    /// </summary
    public abstract void Act(AIController controller);
}
