using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RainyWoods.Extensions;

public class ImageFader : MonoBehaviour
{
    public Image img;
    public float fadeDuration = 1.0f;

    public void FadeIn() => img.CrossFadeAlphaFixed(1.0f, fadeDuration, true);
    public void FadeOut() => img.CrossFadeAlphaFixed(0.0f, fadeDuration, true);

    private void Reset()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        if (!img)
            throw new MissingReferenceException("No image assinged");

        
    }
}
