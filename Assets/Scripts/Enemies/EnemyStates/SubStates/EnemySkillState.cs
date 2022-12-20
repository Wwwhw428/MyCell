using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyAbilityState
{
    public EnemySkillState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void LogicUpdata()
    {
        base.LogicUpdata();

        Enemy.Skill();
    }
}
