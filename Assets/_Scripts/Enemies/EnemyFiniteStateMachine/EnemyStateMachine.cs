using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.EnemyMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }

        public void Initialize(EnemyState startingStates)
        {
            CurrentState = startingStates;
            CurrentState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}