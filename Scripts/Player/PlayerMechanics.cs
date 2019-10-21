using System.Collections;
using UnityEngine;

namespace RainyWoods
{
    public class PlayerMechanics
    {
        //Player connection
        public Player player;

        //Variables needed for this mechanic
        private bool abilityActive;
        private bool abilityAvailable = true;

        /// <summary>This method adds all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate += Update;
        /// </code>
        /// results in <c>Update()</c> being called in Game.cs's Update method.
        /// </example>
        /// </summary>
        public PlayerMechanics()
        {
            Game.Instance.onUpdate += Update;
        }

        /// <summary>
        /// Use Mechanic if possible.
        /// Use Interaction if possible.
        /// Execute Mechanic that is active.
        /// Select active Mechanic.
        /// </summary>
        private void Update()
        {
            if (player.stopped)
                return;

            if (Input.GetButtonDown("Pause"))
            {
                if (player.pauseMenu.activeInHierarchy)
                {
                    player.pauseMenu.SetActive(false);
                    Time.timeScale = 1.0f;
                }
                else
                {
                    player.pauseMenu.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

            //Use Mechanic.
            if (Input.GetButtonDown("L1") && player.mechanics[0].useable && player.mechanics[0].amount != 0)
            {
                player.mechanicIndex = 0;
                abilityActive = true;
                abilityAvailable = false;
                player.mechanics[0].ExecuteMechanic(this);
                player.StartCoroutine(Mechanic(player.mechanics[0].cooldown, 0));
                player.mechanics[0].amount--;
            }
            else if (Input.GetButtonDown("R1") && player.mechanics[1].useable && player.mechanics[1].amount != 0)
            {
                player.mechanicIndex = 1;
                abilityActive = true;
                abilityAvailable = false;
                player.mechanics[1].ExecuteMechanic(this);
                player.StartCoroutine(Mechanic(player.mechanics[1].cooldown, 1));
                player.mechanics[1].amount--;
            }

            //Interact
            if(Input.GetButtonDown("ButtonX") && !player.interact && player.interactAble)
            {
                if(player.activeInteraction != null)
                {
                    player.activeInteraction.Interact(player);
                }
            }

            //Execute Mechanic

        }

        /// <summary>
        /// Wait for duration of the mechanic.
        /// Afterwards cleanup the mechanic.
        /// Wait for the cooldown of the mechanic and then set it available again.
        /// </summary>
        /// <param name="cooldown"></param>
        /// <returns></returns>
        IEnumerator Mechanic(float cooldown, int index)
        {
            yield return new WaitForSeconds(player.mechanics[index].duration);
            player.mechanics[index].useable = false;
            if(player.mechanics[index].cleanUp)
                player.mechanics[index].CleanUpMechanic(this);

            player.animationController.OnPlace();
            yield return new WaitForSeconds(cooldown);
            player.mechanics[index].useable = true;
        }

        /// <summary>This method removes all functions to their respective delegates in Game.cs.
        /// <example>For example:
        /// <code>
        ///    Game.Instance.onUpdate -= Update;
        /// </code>
        /// </example>
        /// </summary>
        ~PlayerMechanics()
        {
            Game.Instance.onUpdate -= Update;
        }
    }
}
