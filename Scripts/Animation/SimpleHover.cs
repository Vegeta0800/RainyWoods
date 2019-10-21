using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods.Extensions;

public class SimpleHover : MonoBehaviour
{
    public float minHover = -1.0f;
    public float maxHover = 1.0f;
    public float speed = 1.0f;
    public float rotationSpeed = 0.0f;
    public Vector3 rotateDirection = Vector3.left;
    public bool unscaledTime = false;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationSpeed > 0.0f)
            transform.Rotate(rotateDirection * rotationSpeed * Time.deltaTime);

        transform.position = new Vector3(startPosition.x, startPosition.y + (float)MathExtension.TimeScaledSin(minHover, maxHover, speed, unscaledTime: unscaledTime), startPosition.z);
    }
}
