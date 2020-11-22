using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State currentState;
        private State startingState;

        internal State CurrentState { get => currentState; set => currentState = value; }
        internal State StartingState { get => startingState; set => startingState = value; }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(State StartingState)
        {
            CurrentState = StartingState;
            StartingState.Enter();
        }

        public void ChangeState(State newState)
        {
            // First exit current state
            currentState.Exit();

            // Change current state
            currentState = newState;

            // Call enter on new state
            currentState.Enter();
        }
    }
}

