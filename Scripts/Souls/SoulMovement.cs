using System;
using System.Collections;
using UnityEngine;

namespace RainyWoods
{
    public class SoulMovement
    {
        //Soul connection
        public Soul soul;

        //Definitions
        public bool onPlayer = false;
        public bool collected = false;
        public bool playerHit = false;
        public bool moveTowardsObject = false;
        public bool moveToGate = false;
        public bool moveToCheckpoint = false;
        public bool onCheckpoint = false;

        private float orbitProgress = 0.0f;
        private float orbitPeriod = 3.0f;
        private GameObject orbitingCenter;


        private float t;

        [HideInInspector]
        public float safeT;

        [HideInInspector]
        public float safeTime = 5.0f;

        [HideInInspector]
        public Transform moveTransform;


        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate += Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public SoulMovement()
        {
            Game.Instance.onUpdate += Update;

            orbitingCenter = GameObject.FindGameObjectWithTag("OrbitingCenter");
        }

        /// <summary>This method handles the movement to the moveTransform each frame.
        /// </summary>
        private void Update()
        {
            if (moveTowardsObject)
            {
                t += soul.moveSpeed * Time.deltaTime;
                safeT += 1.0f * Time.deltaTime;

                if(safeT >= safeTime)
                {
                    soul.transform.Translate((moveTransform.position) * soul.moveSpeed);
                }
                else
                {
                    soul.transform.position = SetPosition(soul.startPosition, moveTransform.position, soul.height, t);
                }
            }

            if(onPlayer)
            {
                t = 0.0f;
                safeT = 0.0f;
            }

            if (!onPlayer && !moveToGate && !moveToCheckpoint)
            {
                moveTransform = orbitingCenter.transform;
            }
        }

        public Vector3 SetPosition(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }

        /// <summary>This method sets the position of the soul with the Ellipse.Calculate()
        /// </summary>
        public void SetPosition()
        {
            if (!moveToGate)
            {
                Vector2 orbitPos = soul.orbitPath.Calculate(orbitProgress);
                soul.transform.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
            }
        }

        /// <summary>Move thhe soul around on the ellipse.
        /// </summary>
        public IEnumerator Orbit()
        {
            if (orbitPeriod < 0.1f)
                orbitPeriod = 0.1f;

            float orbitSpeed = 1f / orbitPeriod;

            while (onPlayer && !moveToGate)
            {
                orbitProgress += Time.deltaTime * orbitSpeed;
                orbitProgress %= 1f;
                SetPosition();
                yield return null;
            }
        }

        /// <summary>This method remove all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate -= Update;
        /// </code>
        /// </example>
        /// </summary>
        public void CleanUp()
        {
            Game.Instance.onUpdate -= Update;
        }
    }
}