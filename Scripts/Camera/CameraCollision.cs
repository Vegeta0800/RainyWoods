using UnityEngine;

namespace RainyWoods
{
    public class CameraCollision
    {
        public CameraController cameraController;

        private Vector3 dir;
        private Vector3 offset;
        private float distance;

        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate = Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public CameraCollision()
        {
            Game.Instance.onStart += LateStart;
            Game.Instance.onUpdate += Update;
        }

        /// <summary>
        /// Late Start is being called after all the Start methods in the Start method of the Game instance.
        /// </summary>
        private void LateStart()
        {
            //Set values
            dir = cameraController.cameraObject.transform.localPosition.normalized;
            distance = cameraController.cameraObject.transform.localPosition.magnitude;
        }

        /// <summary>
        /// Update function
        /// </summary>
        void Update()
        {
            CameraCollisonHandling();
        }

        /// <summary>This method handles the camera collision by setting it back by "distance" when it hits an object with a linecast.
        /// </summary>
        private void CameraCollisonHandling()
        {
            Vector3 desiredCamPos = cameraController.cameraObject.transform.parent.TransformPoint(dir * cameraController.maxDistance);

            RaycastHit hit;
            if (Physics.Linecast(cameraController.cameraObject.transform.parent.position, desiredCamPos, out hit))
            {
                distance = Mathf.Clamp((hit.distance), cameraController.minDistance, cameraController.maxDistance);
            }
            else
            {
                distance = cameraController.maxDistance;
            }

            cameraController.cameraObject.transform.localPosition = Vector3.Lerp(cameraController.cameraObject.transform.localPosition, dir * distance, Time.deltaTime * cameraController.smooth);
        }
    }
}
