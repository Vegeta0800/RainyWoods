using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods;

public abstract class Interaction : MonoBehaviour
{
    //Declare variables needed in all Interactions.
    public bool interactAble = false;
    public float cooldown = 1.0f;

    /// <summary>
    /// Declare an overrideable method for all Interactions. 
    /// </summary>
    /// <param name="player"></param>
    public abstract void Interact(Player player);
}
