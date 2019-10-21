using UnityEngine;
using UnityEngine.UI;

namespace RainyWoods
{
    public class Player : MonoBehaviour
    {
        public enum BlendMode
        {
            Opaque,
            Fade,
        }

        //Needed public variables.
        public Gate gate;
        public Vector3 coffeeOffset = new Vector3( 0, 0, 0);

        public Material[] materials;

        public Text coffeeText;
        public Text coffeeBombText;
        public Text soulText;

        public float timeTakenForFadeOut;

        //Spawnpoint
        [SerializeField] private Transform spawnpoint = null;

        //Player Movement
        #region PlayerMovement
        private PlayerMovement playerMovement;

        public GameObject playerCamera = null;
        public GameObject pauseMenu = null;
        public float speed;
        public float jumpDistance = 0.7f;
        public float jumpCD = 0.3f;
        public float maxSpeed;
        public float turnSpeed;
        public float jumpForce;
        public float jumpDelay;
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public MainCharacterAnimationController animationController;
        #endregion

        #region FollowPlayer
        private FollowPlayer followPlayer;
        public Transform followPlayerObject = null;
        public Transform lampObject = null;
        #endregion

        #region PlayerMechanics
        public Mechanic[] mechanics;
        [HideInInspector] public int mechanicIndex = 0;
        private PlayerMechanics playerMechanics;
        #endregion

        //Player AI Interaction
        [HideInInspector]
        public bool caught = false;
        [HideInInspector]
        public bool disguised = false;
        [HideInInspector]
        public Transform teleportPoint;

        //Interactions
        [HideInInspector]
        public bool interact = false;
        [HideInInspector]
        public Interaction activeInteraction = null;
        [HideInInspector]
        public bool interactAble = false;

        [HideInInspector]
        public bool stopped = false;
        [HideInInspector]
        public float lerpStart = 0.0f;

        public bool fadeOut = false;

        private float duration = 0.0f;
        public float initialDuration = 2.0f;
        public float power = 10.0f;
        public bool shakeItOff = false;

        public Transform cam;

        public bool refillOnDeath = true;

        /// <summary>This method instantiates the player related non Mono Behaviour scripts and connects it with this script.
        /// also sets rigidbody and teleportPoint.
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animationController = GetComponent<MainCharacterAnimationController>();

            #region PlayerMovement
            playerMovement = new PlayerMovement();
            playerMovement.player = this;
            #endregion

            #region FollowPlayer
            followPlayer = new FollowPlayer();
            followPlayer.player = this;
            #endregion

            #region PlayerMechanics
            playerMechanics = new PlayerMechanics();
            playerMechanics.player = this;
            #endregion

            teleportPoint = spawnpoint;
        }

        /// <summary>
        ///Set usage amount for all mechanics.
        /// </summary>
        private void Start()
        {
            Time.timeScale = 1.0f;

            foreach (Mechanic mechanic in mechanics)
            {
                mechanic.amount = mechanic.maxAmount;
                mechanic.useable = true;
            }

            foreach (Material mat in materials)
            {
                ChangeRenderMode(mat, BlendMode.Opaque);
                Color color = mat.color;
                color.a = 1.0f;
                mat.color = color;
            }

            stopped = false;
            duration = initialDuration;

        }

        private void Update()
        {
            coffeeText.text = mechanics[0].amount.ToString();
            coffeeBombText.text = mechanics[1].amount.ToString();
            soulText.text = gate.soulsCollected.ToString() + " / 3";

            if (gate.endSouls)
            {
                gate.endSouls = false;
                stopped = true;
                fadeOut = true;
                lerpStart = Time.time;
                timeTakenForFadeOut = 2.0f;
            }

            if (fadeOut)
            {
                float progress = (Time.time - lerpStart) / timeTakenForFadeOut;

                float alpha = Mathf.SmoothStep(1.0f, 0.0f, progress);
                foreach (Material mat in materials)
                {
                    ChangeRenderMode(mat, BlendMode.Fade);
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }

                if(alpha <= 0.0f)
                {
                    fadeOut = false;
                    gate.gameEnded = true;
                }
            }

            if (shakeItOff)
            {
                if (duration > 0f)
                {
                    cam.position += Random.insideUnitSphere * power;
                    duration -= Time.deltaTime;
                    stopped = true;
                }
                else
                {
                    cam.position = spawnpoint.position;
                    transform.position = spawnpoint.position;
                    shakeItOff = false;
                    duration = initialDuration;
                    stopped = false;
                }
            }
        }

        /// <summary>This method checks if there was a collision between the player and other objects.
        /// <param> Collision collision
        /// <example>For example:
        /// <code>
        ///    if (collision.collider.CompareTag("Enemy") && !caught)
        ///    {
        ///        caught = true;
        ///    }
        /// </code>
        /// results in <c>caught</c> having the value of true.
        /// </example>
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Enemy") && !caught && !collision.collider.GetComponent<AIController>().coffeeInRange)
            {
                shakeItOff = true;
                caught = true;
                gate.soulsCollected = 0;
                gate.SpawnSouls(gate.soulCount);

                if (refillOnDeath)
                {
                    foreach (Mechanic mech in mechanics)
                    {
                        mech.amount = mech.maxAmount;
                        mech.useable = true;
                    }
                }
            }
        }

        /// <summary>
        /// If player walks into an interaction trigger, set the active interaction and make it usable.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Interaction"))
            {
                interactAble = true;
                activeInteraction = other.GetComponent<Interaction>();
            }
        }

        /// <summary>
        /// If player stays in an interaction trigger, set the active interaction and make it usable.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Interaction"))
            {
                interactAble = true;
            }
        }

        /// <summary>
        /// If player walks out of an interaction trigger, set the active interaction to null and make it not usable.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interaction"))
            {
                interactAble = false;
                activeInteraction = null;
            }
        }

        public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = -1;
                    break;
                case BlendMode.Fade:
                    standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
            }

        }
    }
}
