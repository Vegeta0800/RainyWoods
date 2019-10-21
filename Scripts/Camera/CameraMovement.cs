using UnityEngine;

namespace RainyWoods
{
    public class CameraMovement
    {
        public CameraController cameraController;

        private float rotY;
        private float rotX;
        private Vector3 offset;


        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate = Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public CameraMovement()
        {
            Game.Instance.onStart += LateStart;
            Game.Instance.onUpdate += Update;
            Game.Instance.onLateUpdate += LateUpdate;
        }

        private void LateStart()
        {
            cameraController.cameraObject.transform.localPosition = new Vector3(-0.5f, 11.55f, -22.46f);
            cameraController.cameraObject.transform.localEulerAngles = new Vector3(45.0f, 0, -0);

            //Set values
            Vector3 rot = cameraController.transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;

        }

        private void Update()
        {
            Rotate();
        }

        private void LateUpdate()
        {
            Move();
        }

        /// <summary>This method rotates the camera using the right controller stick. Also clamps the view range on the y-axis.
        /// </summary>
        private void Rotate()
        {
            rotY += Input.GetAxis("RSHorizontal") * cameraController.inputSens * Time.deltaTime;
            rotX += Input.GetAxis("RSVertical") * cameraController.inputSens * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -cameraController.clampAngel, cameraController.clampAngel);

            cameraController.transform.rotation = Quaternion.Euler(rotX, rotY, 0.0f);
        }

        /// <summary>This method moves the camera towards the player.
        /// </summary>
        private void Move()
        {
            Vector3 pos = cameraController.player.transform.position;
            pos.y += cameraController.offsetY;
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, pos, cameraController.speed * Time.deltaTime);
        }
    }
}
