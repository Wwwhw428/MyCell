using System.Collections;
using System.Collections.Generic;
using MyCell.CoreSystem;
using MyCell.EnemyMachine.States;
using UnityEngine;

namespace MyCell.EnemyMachine
{
    public class Enemy : MonoBehaviour
    {
        #region State Variable

        protected EnemyStateMachine stateMachine;

        [SerializeField]
        public EnemyData_SO _enemyData;

        #endregion

        #region Check Variable

        // public Transform PlayerCheck
        // {
        //     get => GenericNotImplementedError<Transform>.TryGet(_playerCheck, transform.parent.name);
        //     private set => _playerCheck = value;
        // }
        // [SerializeField]
        // private Transform _playerCheck;
        // public bool CheckPlayerInMinRange
        // {
        //     get => Physics2D.Raycast(PlayerCheck.position, transform.right, _enemyData.PlayerCheckMinRange, _enemyData.WhatIsPlayer);
        // }
        // public bool CheckPlayerInMaxRange
        // {
        //     get => Physics2D.Raycast(PlayerCheck.position, transform.right, _enemyData.PlayerCheckMaxRange, _enemyData.WhatIsPlayer);
        // }

        #endregion

        #region Component

        public Core Core { get; private set; }
        public Animator Anim { get; private set; }

        #endregion

        #region Other Variable
        #endregion

        #region Unity Callback

        protected virtual void Awake()
        {
            stateMachine = new EnemyStateMachine();
            Anim = transform.GetComponent<Animator>();
            Core = GetComponentInChildren<CoreSystem.Core>();
        }

        protected virtual void Start() {}
        
        protected virtual void Update()
        {
            Core.LogicUpdate();
            stateMachine.CurrentState.LogicUpdate();
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.CurrentState.PhysicsUpdate();
        }

        #endregion

        #region Set Function
        #endregion

        #region Other Function

        public void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
        public void AnimationFinishedTrigger() => stateMachine.CurrentState.AnimationFinishedTrigger();
        public virtual void Attack()
        {
        }
        public virtual void RemoteAttack()
        {
        }
        public virtual void Skill()
        {
        }
        #endregion
    }
}