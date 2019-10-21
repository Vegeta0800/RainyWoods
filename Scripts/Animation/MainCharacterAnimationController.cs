using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnimationController : BaseAnimationController
{
    public string jumpTriggerParameter = "OnJump";
    public string placeTriggerParameter = "OnPlace";

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public void OnJump()
    {
        animationController.SetTrigger(jumpTriggerParameter);
    }

    public void OnPlace()
    {
        animationController.SetTrigger(placeTriggerParameter);
    }
}
