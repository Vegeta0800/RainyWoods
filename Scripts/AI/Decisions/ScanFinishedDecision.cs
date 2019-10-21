using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/ScanFinishedDecision")]
public class ScanFinishedDecision : AIDecision
{
    /// <summary>return if the AI has finished scanning the area.
    /// <param> AIController controller
    /// </summary
    public override bool Decide(AIController controller)
    {
        return controller.finishedScanning;
    }

}
