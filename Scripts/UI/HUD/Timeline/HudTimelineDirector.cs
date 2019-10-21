using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using RainyWoods.Extensions;

namespace RainyWoods.UI.HUD.Timeline
{
    public sealed class HudTimelineDirector : MonoBehaviour
    {
        [Header("Controls")]
        public string continueButton = "ButtonX";

        [Header("References")]
        public TextMeshProUGUI targetTextElement;
        public HudTimeline autoStart;
        public GameObject textContainer;

        [Header("Events")]
        public UnityEvent timelineStarted;
        public UnityEvent timelineFinished;

        private HudTimeline currentTimeline;
        private List<HudTimelineElement>.Enumerator timelineEnumerator;
        
        private HudTimelineElement currentElement;
        private float elementDisplayedAt = float.NaN;
        private FadeDirection fadeDirection = FadeDirection.None;
        private bool fadeInDone = false;
        private bool fadeOutDone = false;
        private bool doneWithCurrentElement = false;

        private void Start()
        {
            if (!targetTextElement)
                throw new MissingReferenceException("TextMesh Pro Text element is missing");

            if (autoStart)
                PlayTimeline(autoStart);

            timelineStarted.AddListener(() =>
            {
                textContainer?.SetActive(true);
                Time.timeScale = 0.0f;
            });

            timelineFinished.AddListener(() =>
            {
                textContainer?.SetActive(false);
                Time.timeScale = 1.0f;
            });
        }

        private void Update()
        {
            UpdateCurrentElement();
        }

        public void PlayTimeline(HudTimeline timeline)
        {
            if (!timeline || timeline.timelineElements?.Count == 0)
                return;
            
            currentTimeline = timeline;
            timelineEnumerator = currentTimeline.timelineElements.GetEnumerator();
            timelineStarted?.Invoke();
        }

        private void UpdateCurrentElement()
        {
            // Check for active timeline or break loop if no timeline is present
            if (currentTimeline == null)
                return;
            
            // Move to next element if the current one is done or null
            // Timeline will end when no new element is present
            bool newElement = false;
            if ((currentElement == null || doneWithCurrentElement) && !(newElement = TryMoveNext()))
            {
                timelineFinished?.Invoke();
                return;
            }

            // If we got a new element apply contents and start fading if enabled
            if (newElement)
            {
                targetTextElement.text = currentElement.content;
                if (currentElement.useFadeIn)
                {
                    targetTextElement.color = targetTextElement.color.SetAlpha(0.0f);
                    fadeDirection = FadeDirection.FadeIn;
                }
            }

            // Do effective fading
            switch (fadeDirection)
            {
                case FadeDirection.FadeIn:
                    fadeDirection = DoFadeIn() ? FadeDirection.None : FadeDirection.FadeIn;
                    fadeInDone = fadeDirection == FadeDirection.None;
                    break;

                case FadeDirection.FadeOut:
                    fadeDirection = DoFadeOut() ? FadeDirection.None : FadeDirection.FadeOut;
                    fadeOutDone = fadeDirection == FadeDirection.None;
                    break;

                case FadeDirection.None:
                default:
                    break;
            }

            // Remember when the element was effectively displayed (after fading in, if enabled, or when a new element is displayed)
            if (((currentElement.useFadeIn && fadeInDone) || newElement) && float.IsNaN(elementDisplayedAt))
                elementDisplayedAt = Time.unscaledTime;

            // Check if the display time is over and move to next element or fade out
            //bool continueNext = (Time.time > (elementDisplayedAt + currentElement.displayDuration));
            bool continueNext = Input.GetButtonDown(continueButton) || fadeOutDone;
            if (continueNext)
            {
                if (fadeOutDone || !currentElement.useFadeOut)
                    doneWithCurrentElement = true;
                else
                    fadeDirection = FadeDirection.FadeOut;
            }
        }

        private bool DoFadeIn()
        {
            Color srcColor = targetTextElement.color;
            srcColor.a += currentElement.fadeInSpeed * Time.unscaledDeltaTime;

            targetTextElement.color = srcColor;
            if (targetTextElement.color.a >= 0.99f)
            {
                Color c = targetTextElement.color;
                c.a = 1.0f;

                targetTextElement.color = c;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoFadeOut()
        {
            Color srcColor = targetTextElement.color;
            srcColor.a -= currentElement.fadeOutSpeed * Time.unscaledDeltaTime;

            targetTextElement.color = srcColor;
            if (targetTextElement.color.a <= 0.01f)
            {
                Color c = targetTextElement.color;
                c.a = 0.0f;

                targetTextElement.color = c;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryMoveNext()
        {
            targetTextElement.text = string.Empty;
            if (timelineEnumerator.MoveNext())
            {
                currentElement = timelineEnumerator.Current;
                if (currentElement == null)
                {
                    currentTimeline = null;
                    return false;
                }
                else
                {
                    doneWithCurrentElement = false;
                    fadeInDone = false;
                    fadeOutDone = false;
                    fadeDirection = FadeDirection.None;
                    elementDisplayedAt = float.NaN;

                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}