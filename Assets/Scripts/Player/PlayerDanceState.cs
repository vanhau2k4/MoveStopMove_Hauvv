using System.Collections;
using UnityEngine;

public class PlayerDanceState : PlayerState
{
    public PlayerDanceState(Player _player, PlayerStateMachine _stateMachine, string _animBoolState) : base(_player, _stateMachine, _animBoolState)
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
        if(player.winCheck != true)
        {
            if (joystick.movementDirection.sqrMagnitude > 0) stateMachine.ChangeState(player.playerMoveState);
            if (joystick.movementDirection.sqrMagnitude == 0 && player.target == null) stateMachine.ChangeState(player.playerIdleState);
        }
    }
}
