using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;
using UTools.Engine.Extensions.ClassExtensions;

namespace RainyWoods.UI.HUD.Timeline
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class HudTimelineTrigger : MonoBehaviour
    {
        [Header("References")]
        public HudTimelineDirector targetDirector;
        public HudTimeline playableTimeline;

        [Header("Settings")]
        public bool playOnce = true;
        [Reorderable]
        public List<string> onlyTags;

        private bool played = false;

        private void OnDrawGizmos()
        {
            /*
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
            
            var c = GetComponent<BoxCollider>();
            if (c)
                Gizmos.DrawCube(transform.position + c.center, c.size.Multiply(transform.localScale));
            */
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!playOnce || !played) && (onlyTags.Count == 0 || onlyTags.Contains(other.tag)))
                PlayDirector();
        }

        public void PlayDirector()
        {
            played = true;

            if (targetDirector && playableTimeline)
                targetDirector.PlayTimeline(playableTimeline);
            else
                throw new MissingReferenceException("Target Director and Playable Timeline are required");
        }
    }
}
