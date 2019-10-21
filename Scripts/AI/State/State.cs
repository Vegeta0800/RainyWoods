using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    //Actions and Transitions for the State
    public AIAction[] actions;
    public AITransition[] transitions;

    /// <summary>Execute the Actions from the controller and check for any Transitions.
    /// <param> AIController controller
    /// </summary>
    public void UpdateState(AIController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    /// <summary>Loop through all Actions in the controller and execute the overridden Act function.
    /// <param> AIController controller
    /// </summary>
    private void DoActions(AIController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    /// <summary>Loop through all the Transitions in the controller and check if the Decision for that Transition returns true. 
    /// If so switch the State to the trueState State for that Transition.
    /// If not switch the State to the falseState State for that Transition.
    /// <param> AIController controller
    /// </summary>
    private void CheckTransitions(AIController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            if (transitions[i].decision.Decide(controller))
            {
                controller.TransitionState(transitions[i].trueState);
                break;
            }
            else
            {
                controller.TransitionState(transitions[i].falseState);
            }
        }
    }
}
