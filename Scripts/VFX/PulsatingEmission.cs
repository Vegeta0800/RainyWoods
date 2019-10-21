using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods.Extensions;

namespace RainyWoods.VFX
{
    public class PulsatingEmission : MonoBehaviour
    {
        public Material targetMaterial;

        [Header("Emission")]
        [ColorUsage(showAlpha: false, hdr: true)] public Color baseColor;

        [Range(-100.0f, 100.0f)] public float minIntensity = 0.5f;
        [Range(-100.0f, 100.0f)] public float maxIntensity = 1.0f;

        [Header("Animation")]
        [Range(0.0f, 500.0f)] public float speed = 100.0f;
        [Range(0.0f, 360.0f)] public float offset = 0.0f;
        public bool useOffset = true;
        public bool randomOffset = true;

        private void OnValidate()
        {
            if (useOffset && randomOffset)
                offset = Random.Range(0.0f, 360.0f);
        }

        private void Awake()
        {
            if (!targetMaterial)
                throw new MissingReferenceException("No target material assigned");
        }

        private void Update()
        {
            UpdateEmission();
        }

        private void UpdateEmission()
        {
            float intensity = (float)MathExtension.TimeScaledSin(minIntensity, maxIntensity, speed, offset, false);

            targetMaterial.SetColor("_EmissionColor", baseColor * intensity);
        }
    }
}

