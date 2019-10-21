using UnityEngine;
using UTools.Engine.Extensions.ClassExtensions;

namespace RainyWoods
{
    public class PlayerMovement
    {
        //Player connection
        public Player player;

        //Variables needed for this script.
        private float angle;

        private float jumpAt = 0.0f;

        private float waitTime = 0.0f;
        private bool jumpAvailable = true;

        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate += Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public PlayerMovement()
        {
            Game.Instance.onUpdate += Update;
            Game.Instance.onFixedUpdate += FixedUpdate;
        }

        /// <summary>
        /// Rotate the player every frame.
        /// </summary>
        private void Update()
        {
            if (player.stopped)
                return;
            Rotate();

            if(waitTime <= 0.0f && !jumpAvailable)
            {
                waitTime = player.jumpCD;
                jumpAvailable = true;
            }
            else
            {
                waitTime -= 1.0f * Time.deltaTime;
            }
        }

        /// <summary>
        /// Move or Jump Player every fixed amount of frames.
        /// </summary>
        private void FixedUpdate()
        {
            if (player.stopped)
                return;
            Move();
            RaycastHit hit;
            //Execute Jump() if the A button is pressed on a controller.
            if (Physics.Raycast(player.transform.position, -player.transform.up * 1.4f, out hit, player.jumpDistance) && jumpAvailable)
            {
                if (hit.transform.tag != "Player")
                    if (Input.GetButton("Jump") && jumpAvailable)
                    {
                        jumpAvailable = false;
                        Jump();
                    }
            }

            UpdateJump();

        }

        /// <summary>This method moves the player via setting velocity (which is clamped).
        /// </summary>
        private void Move()
        {
            Vector3 movementForce = player.transform.forward * Time.deltaTime * player.speed;

            if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
                player.rb.AddForce(movementForce, ForceMode.Force);//.velocity = movementForce;

            //player.rb.velocity = player.rb.velocity.ClampAll(-player.maxSpeed, player.maxSpeed);
        }

        /// <summary>This method rotates the player via the camera and the left controller stick.
        /// </summary>
        private void Rotate()
        {
            Quaternion rotation = Quaternion.LookRotation(new Vector3(player.playerCamera.transform.forward.x, 0, player.playerCamera.transform.forward.z));
            if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            {
                angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                angle = Mathf.Rad2Deg * angle;
            }
            Quaternion targetRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + angle, rotation.eulerAngles.z);

            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        }

        /// <summary>
        /// Sets jumpAt to let the player jump after the delay.
        /// </summary>
        private void Jump()
        {
            if (jumpAt == 0.0f)
            {
                jumpAt = Time.time + player.jumpDelay;
                player.animationController?.OnJump();
                jumpAvailable = false;
            }
        }

        /// <summary>
        /// This method lets the player jump into the air via velocity change.
        /// </summary>
        private void UpdateJump()
        {
            if (jumpAt > 0.0f && Time.time >= jumpAt)
            {
                //player.rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * player.jumpForce * Time.deltaTime;
                player.rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
                jumpAt = 0.0f;
            }
        }

        /// <summary>This method removes all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate -= Update;
        /// </code>
        /// </example>
        /// </summary>
        ~PlayerMovement()
        {
            Game.Instance.onUpdate -= Update;
            Game.Instance.onFixedUpdate -= FixedUpdate;
        }
    }
}
