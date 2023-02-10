using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.EnemyMachine.States;

namespace MyCell.EnemyMachine.Specific
{
    public class Enemy_2_IdleState : EnemyIdleState
    {
        public Enemy_2_IdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
        {
        }
    }
}
