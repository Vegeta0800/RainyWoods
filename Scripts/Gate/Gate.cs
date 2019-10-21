using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RainyWoods;

namespace RainyWoods
{
    public class Gate : MonoBehaviour
    {
        //Definitions
       // public Material gateMaterial;
        public float speed;
        public float cooldownTime;
        public Transform gateCenter;
        public List<Transform> soulSpawnpoints;

        [SerializeField] private GameObject soulPrefab = null;
        [SerializeField] private GameObject orbitingCenter = null;
        public int soulCount;

        public string targetLevel;

        [HideInInspector]
        public List<GameObject> collectedSouls;

        [HideInInspector]
        public List<GameObject> souls;

        [HideInInspector]
        public int soulsCollected = 0;

        [HideInInspector]
        public int soulsDelivered = 0;

        private float waitTime;
        private int soulIndex = 0;
        private bool activated;

        [HideInInspector]
        public bool gameEnded;
        public bool endSouls;

        /// <summary>This method sets all variables in this script. And Spawns the Souls.
        /// </summary>
        private void Awake()
        {   
            //gateMaterial.color = Color.grey;
            activated = false;

            SpawnSouls(soulCount);
        }

        /// <summary>This method handles the GateBehaviour. It switches the material color, collects the souls and ends the game. If activated.
        /// </summary>
        private void Update()
        {
            if (activated)
            {
                //gateMaterial.color = Color.red;

                if (soulsDelivered == soulsCollected)
                {
                    endSouls = true;
                    activated = false;
                    //gateMaterial.color = Color.yellow;
                }

                if (waitTime <= 0.0f)
                {
                    if (collectedSouls.Count > soulIndex)
                    {
                        foreach (Collider c in collectedSouls[soulIndex].GetComponents<Collider>())
                        {
                            c.enabled = true;
                        }

                        Soul soul = collectedSouls[soulIndex].GetComponent<Soul>();

                        collectedSouls[soulIndex].transform.SetParent(null);
                        soul.soulMovement.moveToGate = true;
                        soul.soulMovement.onPlayer = false;
                        soul.soulMovement.moveTransform = gateCenter;
                        soul.transform.rotation = Quaternion.Euler(0, 0, 0);
                        soul.soulMovement.moveTowardsObject = true;
                        waitTime = cooldownTime;

                        soulIndex++;
                    }
                    else
                        endSouls = true;
                }
                else
                {
                    waitTime -= 1.0f * Time.deltaTime;
                }
            }
            else if (gameEnded)
            {
                if (!string.IsNullOrEmpty(targetLevel))
                    UnityEngine.SceneManagement.SceneManager.LoadScene(targetLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }

        /// <summary>This method handles trigger collisions with the gate and other objects.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && soulsCollected == soulCount)
            {
                other.GetComponent<Player>().stopped = true;
                waitTime = 0.0f;
                activated = true;
            }
        }

        /// <summary>
        /// Destroy all souls that already exist and then reinstantiate them to random spawnpoints. The count is the amount of souls that shall be spawned.
        /// this method is public so the saveGame can use it onload.
        /// </summary>
        /// <param name="count"></param>
        public void SpawnSouls(int count)
        {
            if (souls.Count != 0)
            {
                for (int i = 0; i < souls.Count; i++)
                {
                    GameObject.Destroy(souls[i]);
                }

                for (int i = 0; i < orbitingCenter.transform.childCount; i++)
                {
                    GameObject.Destroy(orbitingCenter.transform.GetChild(i).gameObject);
                }

                collectedSouls.Clear();
                souls.Clear();
            }

            if (soulSpawnpoints.Count > count)
            {
                List<Transform> tempPoints = new List<Transform>(soulSpawnpoints);

                for (int i = 0; i < count; i++)
                {
                    int randomIndex = Random.Range(0, tempPoints.Count);
                    souls.Add(Instantiate(soulPrefab, tempPoints[randomIndex].position, soulPrefab.transform.rotation));//, tempPoints[randomIndex].rotation));
                    tempPoints.Remove(tempPoints[randomIndex]);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    souls.Add(Instantiate(soulPrefab, soulSpawnpoints[i].position, soulPrefab.transform.rotation));// soulSpawnpoints[i].rotation));
                }
            }

        }
    }
}
