using System.Collections;
using UnityEngine;

namespace RainyWoods
{
    public class Game : MonoBehaviour
    {
        //Delegates for MonoBehaviour functions
        public delegate void OnStart();
        public OnStart onStart;

        public delegate void OnLateStart();
        public OnLateStart onLateStart;

        public delegate void OnUpdate();
        public OnUpdate onUpdate;

        public delegate void OnFixedUpdate();
        public OnFixedUpdate onFixedUpdate;

        public delegate void OnLateUpdate();
        public OnLateUpdate onLateUpdate;

        //Instance;
        private static Game _instance;

        /// <summary>
        /// returns the instance of game.
        /// </summary>
        public static Game Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Disable multiple instances of Game.
        /// </summary>
        private Game()
        {
            _instance = this;
        }

        /// <summary>
        /// Execute all Start functions added onto the delegate.
        /// After that execute all LateStart functions added onto the delegate.
        /// </summary>
        private void Start()
        {
            if (onStart != null)
                onStart();

            if (onLateStart != null)
                onLateStart();
        }

        /// <summary>
        /// Execute all Update functions added onto the delegate
        /// </summary>
        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }

        /// <summary>
        /// Execute all FixedUpdate functions added onto the delegate
        /// </summary>
        private void FixedUpdate()
        {
            if (onFixedUpdate != null)
                onFixedUpdate();
        }

        /// <summary>
        /// Execute all LateUpdate functions added onto the delegate
        /// </summary>
        private void LateUpdate()
        {
            if (onLateUpdate != null)
                onLateUpdate();
        }
    }
}
