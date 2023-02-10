using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.EnemyMachine.States
{
    public class EnemyAbilityState : EnemyState
    {
        public EnemyAbilityState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
        {
        }
    }
}