using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        if (!player)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (!playerObj)
            {
                throw new UnityException("Player not assigned and not found in scene (Searched by tag 'Player')");
            }
            else
            {
                player = playerObj.transform;
            }
        }

        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    private void LateUpdate()
    {
        float lastY = transform.position.y;
        Vector3 lerpedPos = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        lerpedPos.y = lastY;

        transform.position = lerpedPos;
        
    }
}
