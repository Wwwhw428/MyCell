using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAbilityState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void LogicUpdata()
    {
        base.LogicUpdata();

        Enemy.Attack();
    }
}

