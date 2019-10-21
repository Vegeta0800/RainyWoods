using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    public Animator doorAnimator;

    public string openDoorTrigger = "OpenDoor";
    public string closeDoorTrigger = "CloseDoor";

    void Reset()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!doorAnimator)
            throw new MissingReferenceException("Door Animator is required but missing");
    }

    public void OpenDoor()
    {
        doorAnimator?.SetTrigger(openDoorTrigger);
    }

    public void CloseDoor()
    {
        doorAnimator?.SetTrigger(closeDoorTrigger);
    }
}
