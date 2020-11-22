using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    // Abstract Class for state implementation
    public class State
    {
        // Method called on arrival in state
        public virtual void Enter()
        {

        }

        // Special state actions
        public virtual void Update()
        {

        }

        // Special physics updates (not sure if usefull)
        public virtual void PhysicsUpdate()
        {

        }

        // Method called on state exit
        public virtual void Exit()
        {

        }
    }
}
