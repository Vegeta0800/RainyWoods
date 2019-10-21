using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RainyWoods.Extensions;

public class PulsatingImage : MonoBehaviour
{
    public Image targetImage;
    public float speed = 1.0f;

    void Reset()
    {
        targetImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetImage)
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, (float)MathExtension.TimeScaledSin(0.5, 1.0, speed, 0.0, true));
    }
}
