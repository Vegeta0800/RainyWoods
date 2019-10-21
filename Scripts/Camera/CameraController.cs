using UnityEngine;

namespace RainyWoods
{
    public class CameraController : MonoBehaviour
    {
        #region CameraMovement
        private CameraMovement cameraMovement;

        public GameObject player;
        public float inputSens;
        public float clampAngel;
        public float speed;
        public float offsetY;
        #endregion

        #region CameraCollision
        private CameraCollision cameraCollision;

        public GameObject cameraObject;
        public float minDistance;
        public float maxDistance;
        public float smooth;
        #endregion

        /// <summary>This method instantiates the camera related non Mono Behaviour scripts and connects it with this script.
        /// </summary>
        private void Awake()
        {
            #region CameraMovement
            cameraMovement = new CameraMovement();
            cameraMovement.cameraController = this;
            #endregion

            #region CameraCollision
            cameraCollision = new CameraCollision();
            cameraCollision.cameraController = this;
            #endregion
        }

    }
}
