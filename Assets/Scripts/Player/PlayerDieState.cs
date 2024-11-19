using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _stateMachine, string _animBoolState) : base(_player, _stateMachine, _animBoolState)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player.dieCheck != true)
        {
            if (joystick.movementDirection.sqrMagnitude > 0) stateMachine.ChangeState(player.playerMoveState);
            if (joystick.movementDirection.sqrMagnitude == 0 && player.target == null) stateMachine.ChangeState(player.playerIdleState);
        }
    }
}
