using System;
using UnityEngine;

namespace RainyWoods.UI.HUD.Timeline
{
    [Serializable]
    public class HudTimelineElement
    {
        [Tooltip("The name of the element")]
        public string name = "New Element";

        [Header("Content Settings")]
        [Multiline, Tooltip("The contents of the element")]
        public string content = "";

        //[Range(0.1f, 30.0f), Tooltip("The duration the content will be displayed to the player")]
        //public float displayDuration = 5.0f;
        
        [Header("Fading Settings")]
        [Tooltip("Enable to let the text fade in")]
        public bool useFadeIn = true;

        [Tooltip("Enable to let the text fade out")]
        public bool useFadeOut = true;
        
        [Range(0.1f, 30.0f), Tooltip("The duration in seconds the element will take to fade in")]
        public float fadeInSpeed = 1.0f;
        
        [Range(0.1f, 30.0f), Tooltip("The duration in seconds the element will take to fade out")]
        public float fadeOutSpeed = 1.0f;
    }
}
