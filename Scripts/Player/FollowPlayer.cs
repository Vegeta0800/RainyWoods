using UnityEngine;

namespace RainyWoods
{
    public class FollowPlayer
    {
        //Player
        public Player player;

        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate += Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public FollowPlayer()
        {
            Game.Instance.onUpdate += Update;
        }

        /// <summary>
        /// Set rotation and position to the players positon and rotation every frame.
        /// </summary>
        private void Update()
        {
            player.followPlayerObject.rotation = Quaternion.Euler(0, 0, 0);
            player.followPlayerObject.position = player.lampObject.position;
        }

        /// <summary>This method removes all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate -= Update;
        /// </code>
        /// </example>
        /// </summary>
        ~FollowPlayer()
        {
            Game.Instance.onUpdate -= Update;
        }
    }
}
