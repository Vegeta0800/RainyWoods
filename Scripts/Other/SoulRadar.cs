using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainyWoods
{
    public class SoulRadar : MonoBehaviour
    {
        //Icons
        public GameObject soulIcon;
        [SerializeField] private GameObject playerIcon = null;
        private GameObject targetIcon;

        //Intervals and cooldowns
        public float impulsCooldown = 3.0f;

        //Variables used for the position and detection.
        [SerializeField] private GameObject player = null;
        [SerializeField] private Gate gate = null;
        [SerializeField] private float detectionRange = 30.0f;
        private float radarRadius;
        private float cd = 0.0f;

        // Collection for Object Pooling.
        private List<GameObject> soulIconCollection;
        private List<GameObject> souls = new List<GameObject>();

        private List<GameObject> activeSouls = new List<GameObject>();

        /// <summary>
        /// Sets all needed variables. Also starts the Interval for deleting the Icons.
        /// </summary>
        private void Start()
        {
            souls = gate.souls;

            targetIcon = GameObject.Instantiate(playerIcon);
            targetIcon.transform.SetParent(transform, false);
            targetIcon.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);

            // Get the radius of radar.
            radarRadius = GetComponent<RectTransform>().sizeDelta.x / 2.0f;

            // Start Object Pooling.
            soulIconCollection = new List<GameObject>();
            StartCoroutine(DestroyUnusedIcons(impulsCooldown));
        }

        /// <summary>
        /// Update. Show the player icon and if available, add all active souls, set cooldown, hide all icons and create all soul icons.
        /// </summary>
        private void Update()
        {
            PlotPlayerIcon();

            if (cd <= 0.0f)
            {
                activeSouls.Clear();

                foreach (GameObject soul in souls)
                {
                    if (!soul.GetComponent<Soul>().soulMovement.onPlayer && !soul.GetComponent<Soul>().soulMovement.onCheckpoint)
                    {
                        activeSouls.Add(soul);
                    }
                }

                cd = impulsCooldown;

                // Hide all icons.
                HideAllIcons();

                // Plot all souls within range.
                foreach (GameObject soul in activeSouls)
                {
                    if (Vector3.Distance(player.transform.position, soul.transform.position) < detectionRange)
                    {
                        PlotSoulIcons(soul.transform.position);
                    }
                }
            }
            else
            {
                cd -= 1.0f * Time.deltaTime;
            }
        }

        /// <summary>
        /// Hide all icons.
        /// </summary>
        private void HideAllIcons()
        {
            foreach (GameObject soulIcon in soulIconCollection)
            {
                soulIcon.SetActive(false);
            }
        }

        /// <summary>
        /// Plot all souls within range.
        /// </summary>
        /// <param name="soulPos">The position of soul.</param>
        private void PlotSoulIcons(Vector3 soulPos)
        {
            Vector3 plotPos = (soulPos - player.transform.position) * (radarRadius / detectionRange);
            RectTransform iconRect = GetSoulIcon().GetComponent<RectTransform>();
            iconRect.anchoredPosition = new Vector2(plotPos.x, plotPos.z);
        }

        /// <summary>
        /// Plot the player icon.
        /// </summary>
        private void PlotPlayerIcon()
        {
            RectTransform iconRect = targetIcon.GetComponent<RectTransform>();
            iconRect.anchoredPosition = new Vector2(0, 0);
        }

        /// <summary>
        /// Get soul icon. Instantiate every new icon. Return old icons that havent changed.
        /// </summary>
        /// <returns>The soul object.</returns>
        private GameObject GetSoulIcon()
        {
            foreach (GameObject oldSoulIcon in soulIconCollection)
            {
                if (oldSoulIcon.activeSelf == false)
                {
                    oldSoulIcon.SetActive(true);
                    return oldSoulIcon;
                }
            }

            GameObject newSoulIcon = GameObject.Instantiate(soulIcon);
            newSoulIcon.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);
            newSoulIcon.transform.SetParent(transform, false);
            soulIconCollection.Add(newSoulIcon);

            return newSoulIcon;
        }

        /// <summary>
        /// Destroy unused icons.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <returns></returns>
        private IEnumerator DestroyUnusedIcons(float interval)
        {
            while (true)
            {
                List<GameObject> soulIconRemove = new List<GameObject>();
                foreach (GameObject soulRemove in soulIconCollection)
                {
                    soulIconRemove.Add(soulRemove);
                }
                foreach (GameObject soulRemove in soulIconRemove)
                {
                        soulIconCollection.Remove(soulRemove);
                        Destroy(soulRemove);
                }

                yield return new WaitForSeconds(interval);
            }
        }

        /// <summary>
        /// Set the detection range.
        /// </summary>
        /// <param name="detectionRange">The detection range.</param>
        public void SetDetectionRange(float detectionRange)
        {
            this.detectionRange = detectionRange;
        }

        /// <summary>
        /// Get the detection range.
        /// </summary>
        /// <returns>The detection range.</returns>
        public float GetDetectionRange()
        {
            return this.detectionRange;
        }
    }
}
