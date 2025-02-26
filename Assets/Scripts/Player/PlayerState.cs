using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    public MainJoystick joystick;
    private string animBoolName;

    protected bool triggerCalled;
    public SouscePlay souscePlay;
    public PlayerState(Player _player,PlayerStateMachine _stateMachine,string _animBoolState)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolState;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        joystick = player.joystick;
        souscePlay = player.souscePlay;
    }
    public virtual void Update()
    {
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
