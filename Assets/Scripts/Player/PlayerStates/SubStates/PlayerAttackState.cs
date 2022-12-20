using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Weapon;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    public PlayerAttackState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName, Weapon weapon) : base(player, statesMachine, playerData, animBoolName)
    {
        this._weapon = weapon;
        _weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocity(0, 0);
        _weapon.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocity(0, 0);
    }

    public void ExitHandler()
    {
        isAbilityDone = true;
    }
}
