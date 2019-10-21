using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITransition
{
    //Declare a Decision for that Transition and a State for if the Decision returns true and one for if it returns false.
    public AIDecision decision;
    public State trueState;
    public State falseState;
}
