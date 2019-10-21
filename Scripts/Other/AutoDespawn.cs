using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    public float afterSeconds = 1.0f;
    private float startedAt;

    void Start()
    {
        startedAt = Time.time;
    }

    void Update()
    {
        if (Time.time > startedAt + afterSeconds)
            Destroy(gameObject);
    }
}
