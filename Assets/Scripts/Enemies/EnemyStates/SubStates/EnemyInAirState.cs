using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInAirState : EnemyState
{
    public EnemyInAirState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }
}

