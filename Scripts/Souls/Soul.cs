using UnityEngine;
using UTools.Engine.Extensions.ClassExtensions;

namespace RainyWoods
{
    public class Soul : MonoBehaviour
    {
        //Definitions
        [HideInInspector]
        public SoulMovement soulMovement;
        private Rigidbody rb;

        public float moveSpeed = 10.0f;
        public float rotateSpeed = 20.0f;
        public float height = 3.0f;
        [HideInInspector] public Vector3 startPosition;
        public Ellipse orbitPath = null;

        private Gate gate = null;
        private GameObject orbitingCenter;

        /// <summary>This method instantiates the soul related non Mono Behaviour scripts and connects it with this script.
        /// Also finds the orbitingCenter, Gate and rigidbody.
        /// </summary>
        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            soulMovement = new SoulMovement();
            soulMovement.soul = this;

            orbitingCenter = GameObject.FindGameObjectWithTag("OrbitingCenter");
            gate = GameObject.FindGameObjectWithTag("Gate").GetComponent<Gate>();
        }

        private void Update()
        {
            if(soulMovement.safeT >= soulMovement.safeTime)
            {
                soulMovement.safeT = 0.0f;
                SafeHit();
            }
        }

        /// <summary>This method checks if there was a collision between the Soul and other objects.
        /// <param> Collision collision
        /// <example>For example:
        /// <code>
        ///    else if (collision.gameObject.tag == "Gate")
        ///    {
        ///         GameObject.Find("Gate").GetComponent<Gate>().soulsDelivered++;
        ///        this.gameObject.SetActive(false);
        ///   }
        /// </code>
        /// </example>
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && !soulMovement.playerHit)
            {
                soulMovement.playerHit = true;
                transform.localScale = transform.localScale * 0.1f;//new Vector3(0.15f, 0.15f, 0.15f);
                transform.rotation = Quaternion.Euler(-90, 0, 0);
                transform.SetParent(orbitingCenter.transform);
                rb.velocity = Vector3.zero;
                soulMovement.moveTowardsObject = false;
                soulMovement.onPlayer = true;
                soulMovement.moveToCheckpoint = false;

                gate.collectedSouls.Add(gameObject);
                gate.soulsCollected++;

                foreach (Collider c in GetComponents<Collider>())
                {
                    c.enabled = false;
                }

                soulMovement.SetPosition();
                StartCoroutine(soulMovement.Orbit());
            }
            else if (collision.gameObject.tag == "Gate")
            {
                gate.soulsDelivered++;
                this.gameObject.SetActive(false);
            }
        }

        /// <summary>This method checks if there was a trigger collision between the Soul and other objects.
        /// <param> Collision collision
        /// <example>For example:
        /// <code>
        /// if (other.tag == "Player" && !soulMovement.collected)
        /// {
        ///     soulMovement.collected = true;
        ///     soulMovement.moveTowardsObject = true;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !soulMovement.collected)
            {
                soulMovement.collected = true;
                soulMovement.moveTowardsObject = true;
                startPosition = transform.position;
            }
        }

        /// <summary>
        /// If Gameobject is destroyed, cleanup the other scripts in Soul.
        /// </summary>
        private void OnDestroy()
        {
            soulMovement.CleanUp();
            soulMovement = null;
        }

        private void SafeHit()
        {
            soulMovement.playerHit = true;
            transform.localScale = transform.localScale * 0.1f;//new Vector3(0.15f, 0.15f, 0.15f);
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            transform.SetParent(orbitingCenter.transform);
            rb.velocity = Vector3.zero;
            soulMovement.moveTowardsObject = false;
            soulMovement.onPlayer = true;
            soulMovement.moveToCheckpoint = false;

            gate.collectedSouls.Add(gameObject);
            gate.soulsCollected++;

            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }

            soulMovement.SetPosition();
            StartCoroutine(soulMovement.Orbit());
        }
    }
}