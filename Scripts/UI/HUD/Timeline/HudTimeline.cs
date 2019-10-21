using System;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;

namespace RainyWoods.UI.HUD.Timeline
{
    [CreateAssetMenu(fileName = "HudTimeline", menuName = "Rainy Woods/Create Hud Timeline", order = 1)]
    [Serializable]
    public sealed class HudTimeline : ScriptableObject
    {
        [Reorderable]
        public List<HudTimelineElement> timelineElements;
    }
}